using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GM.Model
{
    /// <summary>
    ///     
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


        public static IEnumerable<Player> GrabPlayers ()
        {
            var additionals = GrabPlayersAdditionalInfo();
            var players = GrabPlayerBasicInfo().ToList();
            
            var query = from player in players
                              from info in additionals
                                  .Where(info => info.Name == player.Name && info.Franchise.Equals(player.Franchise))
                              select new { player, info };
            
            

            var result = new List<Player>();
            foreach (var match in query)
            {
                var player = match.player;

                player.SetAdditionals(match.info);
                player.SetEliteProspectsId(GetEliteProspectsId(player.Name, player.Birthday));

                result.Add(player);
            }
            
            return result;
        }

        private static string GetEliteProspectsId (string name, DateTime birthday)
        {
            var address = string.Format("http://api.eliteprospects.com/beta/search?type=player&q={0}&filter=dateOfBirth={1}",
                                        name,
                                        birthday.ToString("yyyy-MM-dd"));

            HttpWebRequest request = ( HttpWebRequest ) WebRequest.Create(address);
            using ( HttpWebResponse response = ( HttpWebResponse ) request.GetResponse() )
            {
                try
                {
                    if ( response.StatusCode != HttpStatusCode.OK )
                    {
                        return null;
                    }

                    // Fetch html document
                    Stream receiveStream = response.GetResponseStream();

                    using (StreamReader streamReader = new StreamReader(receiveStream))
                    {
                        using (JsonTextReader reader = new JsonTextReader(streamReader))
                        {
                            JObject o2 = (JObject)JToken.ReadFrom(reader);
                            var players = o2["players"];
                            var data = players?["data"]?[0];
                            var id = data?["id"];

                            if (id == null)
                            {
                                Trace.WriteLine("No match for " + name + ", born " + birthday.ToString("yyyy-MM-dd"));
                            }
                            return id?.ToString();

                        }
                    }



                }
                finally
                {
                    response.Close();
                }
            }
            return null;
        }


        public static IEnumerable<AdditionalPlayerInfo> GrabPlayersAdditionalInfo ()
        {
            var additionalsDoc = LoadRawData(AdditionalsAddress);

            List<AdditionalPlayerInfo> playersInfos = new List<AdditionalPlayerInfo>();
            List<string> headers = new List<string>();
            foreach ( HtmlNode headerNode in additionalsDoc.DocumentNode.SelectNodes(XPathForAdditionalsHeaders) )
            {
                if ( headers.Contains(headerNode.InnerHtml) )
                {
                    break;
                }
                headers.Add(headerNode.InnerHtml);
            }

            // create player info list
            foreach ( HtmlNode node in additionalsDoc.DocumentNode.SelectNodes(XPathForAdditionals) )
            {

                if ( node.ChildNodes.Count != headers.Count )
                {
                    continue;
                }

                AdditionalPlayerInfo playerInfo = CreatePlayerInfo(headers, node);
                playersInfos.Add(playerInfo);
            }

            return playersInfos;
        }

        /// <summary>
        ///     Grabs all players from the defined address.
        /// </summary>
        private static IEnumerable<Player> GrabPlayerBasicInfo()
        {
            var basicDoc = LoadRawData(BasicsAddress);

            List<Player> players = new List<Player>();

            List<string> skaterHeaders = new List<string>();
            foreach ( HtmlNode skaterHeaderNode in basicDoc.DocumentNode.SelectNodes(XPathForSkaterHeaders) )
            {
                if ( skaterHeaders.Contains(skaterHeaderNode.InnerHtml) )
                {
                    break;
                }
                skaterHeaders.Add(skaterHeaderNode.InnerHtml);
            }

            List<string> goalieHeaders = new List<string>();
            foreach ( HtmlNode goalieHeaderNode in basicDoc.DocumentNode.SelectNodes(XPathForGoalieHeaders) )
            {
                if ( goalieHeaders.Contains(goalieHeaderNode.InnerHtml) )
                {
                    break;
                }
                goalieHeaders.Add(goalieHeaderNode.InnerHtml);
            }

            // create skater list
            foreach ( HtmlNode skaterNode in basicDoc.DocumentNode.SelectNodes(XPathForSkaters) )
            {
                Skater skater = CreateSkater(skaterHeaders, skaterNode);
                players.Add(skater);
            }

            // create goalie list
            foreach ( HtmlNode goalieNode in basicDoc.DocumentNode.SelectNodes(XPathForGoalies) )
            {
                Goalie goalie = CreateGoalie(goalieHeaders, goalieNode);
                players.Add(goalie);
            }

            return players;
        }

        private static HtmlDocument LoadRawData (string address)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
            HtmlDocument basicDoc = new HtmlDocument();
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                try
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return null;
                    }

                    // Fetch html document
                    Stream receiveStream = response.GetResponseStream();
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

        private static Goalie CreateGoalie(List<string> headers, HtmlNode goalieNode)
        {
            Dictionary<string, string> values = ExtractValues(headers, goalieNode);

            Team team = GetTeam(goalieNode);
            return new Goalie(team, values);
        }

        private static Skater CreateSkater(List<string> headers, HtmlNode skaterNode)
        {
            Dictionary<string, string> values = ExtractValues(headers, skaterNode);
            Team team = GetTeam(skaterNode);
            return new Skater(team, values);
        }

        private static AdditionalPlayerInfo CreatePlayerInfo (List<string> headers, HtmlNode node)
        {
            Dictionary<string, string> values = ExtractValues(headers, node);
            Team team = GetFranchise(node);
            return new AdditionalPlayerInfo(team, values);
        }

        private static Dictionary<string, string> ExtractValues (List<string> headers, HtmlNode node)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            
            for (int i = 0; i < headers.Count; i++)
            {
                values[headers.ElementAt(i)] = node.SelectSingleNode("td[" + (i+1) + "]").InnerHtml;
            }
            return values;
        }

        private static Team GetTeam(HtmlNode node)
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

        private static Team GetFranchise(HtmlNode node)
        {
            var divNode = node.Ancestors("div").Last();
            string teamName = divNode.Attributes["id"].Value.Split('_').Last();

            return new Team(teamName);
        }

        #endregion
    }
}
