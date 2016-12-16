using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GM.Persistence;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace GM.Model
{
    /// <summary>
    ///     Grabs all relevant data from the different websites
    /// </summary>
    internal static class DataGrabber
    {
        private const string BasicsAddress = "http://nfhl.eu.dd25712.kasserver.com/2/NFHL-ProTeamRoster.html";
        private const string AdditionalsAddress = "http://nfhl.eu.dd25712.kasserver.com/2/NFHL-ProTeamPlayersInfo.html";
        private const string XPathForAdditionals = "//table[@class='basictablesorter']/tr";
        private const string XPathForAdditionalsHeaders = "//table[@class='basictablesorter']/thead/tr/th";

        private const string XPathForSkaters = "//table[@class='basictablesorter STHSRoster_PlayersTable']/tr";
        private const string XPathForSkaterHeaders = "//table[@class='basictablesorter STHSRoster_PlayersTable']/thead/tr/th";
        private const string XPathForGoalies = "//table[@class='basictablesorter STHSRoster_GoaliesTable']/tr";
        private const string XPathForGoalieHeaders = "//table[@class='basictablesorter STHSRoster_GoaliesTable']/thead/tr/th";

        public static ConcurrentQueue<Player> Players { get; private set; }
        public static IReadOnlyList<Team> Teams { get; private set; }

        public static void GrabPlayers ()
        {
            var additionals = GrabPlayersAdditionalInfo();
            var players = GrabPlayerBasicInfo().ToList();

            var query = from player in players
                        from info in additionals
                            .Where(info => info.Name == player.Name && info.Franchise.Equals(player.Franchise))
                        select new { player, info };

            query = query.ToList();

            var result = new List<Player>();
            foreach (var match in query)
            {
                var player = match.player;
                player.SetAdditionals(match.info);
                result.Add(player);
            }

            Update(result);
        }

        private static void Update (List<Player> players)
        {
            Players = new ConcurrentQueue<Player>(players);
            Teams = Players.Select(p => p.Team).Distinct().ToList();
        }

        public static void GetEliteProspectsId ()
        {
            var playersAdresses = new ConcurrentDictionary<Player, string>();
            Parallel.ForEach(Players,
                             player =>
                                 playersAdresses.TryAdd(player,
                                                        $"http://api.eliteprospects.com/beta/search?type=player&q={player.Name}&filter=dateOfBirth={player.Birthday.ToString("yyyy-MM-dd")}"));
            
            Parallel.ForEach(playersAdresses,new ParallelOptions { MaxDegreeOfParallelism = 10 },
                             kvp =>
                             {
                                 using (var client = new HttpClient())
                                 {
                                     var response = client.GetStringAsync(kvp.Value).Result;
                                     var obj = JToken.Parse(response);
                                     var players = obj["players"];
                                     var data = players?["data"]?[0];
                                     var id = data?["id"];
                                     kvp.Key.SetEliteProspectsId(id?.ToString());
                                 }
                             });

        }

        public static void LoadFromCsv (string path)
        {
            var csvImport = new CsvImport();
            var players = csvImport.Load(path);
            Update(players);
        }

        public static IEnumerable<AdditionalPlayerInfo> GrabPlayersAdditionalInfo ()
        {
            var additionalsDoc = LoadRawData(AdditionalsAddress);

            var playersInfos = new List<AdditionalPlayerInfo>();
            var headers = new List<string>();
            foreach (var headerNode in additionalsDoc.DocumentNode.SelectNodes(XPathForAdditionalsHeaders))
            {
                if (headers.Contains(headerNode.InnerHtml))
                {
                    break;
                }
                headers.Add(headerNode.InnerHtml);
            }

            // create player info list
            foreach (var node in additionalsDoc.DocumentNode.SelectNodes(XPathForAdditionals))
            {
                if (node.ChildNodes.Count != headers.Count)
                {
                    continue;
                }

                var playerInfo = CreatePlayerInfo(headers, node);
                playersInfos.Add(playerInfo);
            }

            return playersInfos;
        }

        /// <summary>
        ///     Grabs all players from the defined address.
        /// </summary>
        private static IEnumerable<Player> GrabPlayerBasicInfo ()
        {
            var basicDoc = LoadRawData(BasicsAddress);

            var players = new List<Player>();

            var skaterHeaders = new List<string>();
            foreach (var skaterHeaderNode in basicDoc.DocumentNode.SelectNodes(XPathForSkaterHeaders))
            {
                if (skaterHeaders.Contains(skaterHeaderNode.InnerHtml))
                {
                    break;
                }
                skaterHeaders.Add(skaterHeaderNode.InnerHtml);
            }

            var goalieHeaders = new List<string>();
            foreach (var goalieHeaderNode in basicDoc.DocumentNode.SelectNodes(XPathForGoalieHeaders))
            {
                if (goalieHeaders.Contains(goalieHeaderNode.InnerHtml))
                {
                    break;
                }
                goalieHeaders.Add(goalieHeaderNode.InnerHtml);
            }

            // create skater list
            foreach (var skaterNode in basicDoc.DocumentNode.SelectNodes(XPathForSkaters))
            {
                var skater = CreateSkater(skaterHeaders, skaterNode);
                players.Add(skater);
            }

            // create goalie list
            foreach (var goalieNode in basicDoc.DocumentNode.SelectNodes(XPathForGoalies))
            {
                var goalie = CreateGoalie(goalieHeaders, goalieNode);
                players.Add(goalie);
            }

            return players;
        }

        private static HtmlDocument LoadRawData (string address)
        {
            var request = (HttpWebRequest)WebRequest.Create(address);
            var basicDoc = new HtmlDocument();
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                try
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return null;
                    }

                    // Fetch html document
                    var receiveStream = response.GetResponseStream();
                    basicDoc.Load(receiveStream);
                }
                finally
                {
                    response.Close();
                }
            }
            return basicDoc;
        }

        #region Private Methods

        private static Goalie CreateGoalie (List<string> headers, HtmlNode goalieNode)
        {
            var values = ExtractValues(headers, goalieNode);

            var team = GetTeam(goalieNode);
            return new Goalie(team, values);
        }

        private static Skater CreateSkater (List<string> headers, HtmlNode skaterNode)
        {
            var values = ExtractValues(headers, skaterNode);
            var team = GetTeam(skaterNode);
            return new Skater(team, values);
        }

        private static AdditionalPlayerInfo CreatePlayerInfo (List<string> headers, HtmlNode node)
        {
            var values = ExtractValues(headers, node);
            var team = GetFranchise(node);
            return new AdditionalPlayerInfo(team, values);
        }

        private static Dictionary<string, string> ExtractValues (List<string> headers, HtmlNode node)
        {
            var values = new Dictionary<string, string>();

            for (int i = 0; i < headers.Count; i++)
            {
                values[headers.ElementAt(i)] = node.SelectSingleNode("td[" + (i + 1) + "]").InnerHtml;
            }
            return values;
        }

        private static Team GetTeam (HtmlNode node)
        {
            var divNode = node.Ancestors("div").Last();
            string teamName = divNode.Attributes["id"].Value.Split('_').Last();
            var header = divNode.SelectSingleNode("h2");
            bool isPro = false;

            if (header != null)
            {
                isPro = header.InnerHtml.Contains("Pro");
            }

            if (isPro)
            {
                return new Team(teamName);
            }
            else
            {
                var parent = node.SelectNodes("parent::*").Single();
                var grandParent = parent.SelectNodes("parent::*").Single();
                var proTeamNode = grandParent.SelectNodes("preceding-sibling::h1").Last();

                string proTeamName = proTeamNode.InnerText;

                return new FarmTeam(teamName, new Team(proTeamName));
            }
        }

        private static Team GetFranchise (HtmlNode node)
        {
            var divNode = node.Ancestors("div").Last();
            string teamName = divNode.Attributes["id"].Value.Split('_').Last();

            return new Team(teamName);
        }

        #endregion
    }
}