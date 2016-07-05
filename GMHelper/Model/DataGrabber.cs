using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace GM.Model
{
    /// <summary>
    ///     
    /// </summary>
    internal static class DataGrabber
    {
        private const string Address = "http://nfhl.eu.dd25712.kasserver.com/2/NFHL-ProTeamRoster.html";
        private const string XPathForSkaters = "//table[@class='basictablesorter STHSRoster_PlayersTable']/tr";
        private const string XPathForSkaterHeaders = "//table[@class='basictablesorter STHSRoster_PlayersTable']/thead/tr/th";
        private const string XPathForGoalies = "//table[@class='basictablesorter STHSRoster_GoaliesTable']/tr";
        private const string XPathForGoalieHeaders = "//table[@class='basictablesorter STHSRoster_GoaliesTable']/thead/tr/th";

        /// <summary>
        ///     Grabs all players from the defined address.
        /// </summary>
        public static IEnumerable<Player> GrabPlayers()
        {
            List<Player> players = new List<Player>();

            // establish connection to website and close it directly afterwards
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Address);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                // Fetch html document
                Stream receiveStream = response.GetResponseStream();
                HtmlDocument doc = new HtmlDocument();
                doc.Load(receiveStream);


                List<string> skaterHeaders = new List<string>();
                foreach ( HtmlNode skaterHeaderNode in doc.DocumentNode.SelectNodes(XPathForSkaterHeaders) )
                {
                    if (skaterHeaders.Contains(skaterHeaderNode.InnerHtml))
                    {
                        break;
                    }
                    skaterHeaders.Add(skaterHeaderNode.InnerHtml);
                }

                List<string> goalieHeaders = new List<string>();
                foreach ( HtmlNode goalieHeaderNode in doc.DocumentNode.SelectNodes(XPathForGoalieHeaders) )
                {
                    if ( goalieHeaders.Contains(goalieHeaderNode.InnerHtml) )
                    {
                        break;
                    }
                    goalieHeaders.Add(goalieHeaderNode.InnerHtml);
                }

                // create skater list
                foreach (HtmlNode skaterNode in doc.DocumentNode.SelectNodes(XPathForSkaters))
                {
                    Skater skater = CreateSkater(skaterHeaders, skaterNode);
                    players.Add(skater);
                }

                // create goalie list
                foreach (HtmlNode goalieNode in doc.DocumentNode.SelectNodes(XPathForGoalies))
                {
                    Goalie goalie = CreateGoalie(goalieHeaders, goalieNode);
                    players.Add(goalie);
                }

                response.Close();
            }

            return players;
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

        private static Dictionary<string, string> ExtractValues (List<string> headers, HtmlNode node)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            for (int i = 0; i < headers.Count; i++)
            {
                values[headers.ElementAt(i)] = node.SelectSingleNode("td[" + (i+1) + "]").InnerHtml;
            }
            return values;
        }

        private static Team GetTeam(HtmlNode goalieNode)
        {
            var divNode = goalieNode.Ancestors("div").Last();
            string teamName = divNode.Attributes["id"].Value.Split('_').Last();
            var header = divNode.SelectSingleNode("h2");
            bool isPro = false;
            if (header != null)
            {
                isPro = header.InnerHtml.Contains("Pro");
            }
            Team team = new Team(teamName, isPro);
            return team;
        }

        #endregion
    }
}
