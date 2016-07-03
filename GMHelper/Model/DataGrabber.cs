﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GM.Model
{
    internal class DataGrabber: IDataGrabber
    {
        private readonly string _address;
        private string _data;

        public DataGrabber(string address)
        {
            _address = address;
        }

        public string Data
        {
            get
            {
                return _data;
            }
            private set
            {
                _data = value;
            }
        }
        

        public void Grab()
        {
            StringBuilder output = new StringBuilder();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_address);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return;
            }

            Stream receiveStream = response.GetResponseStream();

            HtmlDocument doc = new HtmlDocument();
            doc.Load(receiveStream);

            List<Player> players = new List<Player>();
            //Team
            foreach (HtmlNode skaterNode in doc.DocumentNode.SelectNodes("//table[@class='basictablesorter STHSRoster_PlayersTable']/tr"))
            {
                Skater skater = new Skater();
                var ancestors = skaterNode.Ancestors("div");
                skater.Team = ancestors.Last().Attributes["id"].Value.Split('_').Last();

                skater.Name = skaterNode.SelectSingleNode("td[2]").InnerHtml;
                skater.IsCenter = !string.IsNullOrWhiteSpace(skaterNode.SelectSingleNode("td[3]").InnerHtml);
                skater.IsLeftWinger = !string.IsNullOrWhiteSpace(skaterNode.SelectSingleNode("td[4]").InnerHtml);
                skater.IsRightWinger = !string.IsNullOrWhiteSpace(skaterNode.SelectSingleNode("td[5]").InnerHtml);
                skater.IsDefender = !string.IsNullOrWhiteSpace(skaterNode.SelectSingleNode("td[6]").InnerHtml);

                double condition;
                double.TryParse(skaterNode.SelectSingleNode("td[7]").InnerHtml, out condition);
                skater.Condition = condition;

                skater.IsInjured = !string.IsNullOrWhiteSpace(skaterNode.SelectSingleNode("td[8]").InnerHtml);

                int value;
                int.TryParse(skaterNode.SelectSingleNode("td[7]").InnerHtml, out value);
                skater.Checking = value;

                int.TryParse(skaterNode.SelectSingleNode("td[9]").InnerHtml, out value);
                skater.Checking = value;

                int.TryParse(skaterNode.SelectSingleNode("td[10]").InnerHtml, out value);
                skater.Fighting = value;

                int.TryParse(skaterNode.SelectSingleNode("td[11]").InnerHtml, out value);
                skater.Discipline = value;

                int.TryParse(skaterNode.SelectSingleNode("td[12]").InnerHtml, out value);
                skater.Skating = value;

                int.TryParse(skaterNode.SelectSingleNode("td[13]").InnerHtml, out value);
                skater.Strength = value;

                int.TryParse(skaterNode.SelectSingleNode("td[14]").InnerHtml, out value);
                skater.Endurance = value;

                int.TryParse(skaterNode.SelectSingleNode("td[15]").InnerHtml, out value);
                skater.Durability = value;

                int.TryParse(skaterNode.SelectSingleNode("td[16]").InnerHtml, out value);
                skater.PuckHandling = value;

                int.TryParse(skaterNode.SelectSingleNode("td[17]").InnerHtml, out value);
                skater.FaceOff = value;

                int.TryParse(skaterNode.SelectSingleNode("td[18]").InnerHtml, out value);
                skater.Passing = value;

                int.TryParse(skaterNode.SelectSingleNode("td[19]").InnerHtml, out value);
                skater.Scoring = value;

                int.TryParse(skaterNode.SelectSingleNode("td[20]").InnerHtml, out value);
                skater.Defense = value;

                int.TryParse(skaterNode.SelectSingleNode("td[21]").InnerHtml, out value);
                skater.PenaltyShot = value;

                int.TryParse(skaterNode.SelectSingleNode("td[22]").InnerHtml, out value);
                skater.Experience = value;

                int.TryParse(skaterNode.SelectSingleNode("td[23]").InnerHtml, out value);
                skater.Leadership = value;

                int.TryParse(skaterNode.SelectSingleNode("td[24]").InnerHtml, out value);
                skater.Morale = value;

                int.TryParse(skaterNode.SelectSingleNode("td[25]").InnerHtml, out value);
                skater.TA = value;

                int.TryParse(skaterNode.SelectSingleNode("td[26]").InnerHtml, out value);
                skater.SP = value;

                int.TryParse(skaterNode.SelectSingleNode("td[27]").InnerHtml, out value);
                skater.Age = value;

                int.TryParse(skaterNode.SelectSingleNode("td[28]").InnerHtml, out value);
                skater.Contract = value;

                skater.Salary = skaterNode.SelectSingleNode("td[29]").InnerHtml;

                players.Add(skater);
            }
            
            foreach (HtmlNode goalieNode in doc.DocumentNode.SelectNodes("//table[@class='basictablesorter STHSRoster_GoaliesTable']/tr"))
            {
                Goalie goalie = new Goalie();
                var ancestors = goalieNode.Ancestors("div");
                goalie.Team = ancestors.Last().Attributes["id"].Value.Split('_').Last();

                goalie.Name = goalieNode.SelectSingleNode("td[1]").InnerHtml;

                double condition;
                double.TryParse(goalieNode.SelectSingleNode("td[3]").InnerHtml, out condition);
                goalie.Condition = condition;

                goalie.IsInjured = !string.IsNullOrWhiteSpace(goalieNode.SelectSingleNode("td[4]").InnerHtml);

                int value;
                int.TryParse(goalieNode.SelectSingleNode("td[5]").InnerHtml, out value);
                goalie.Skating = value;

                int.TryParse(goalieNode.SelectSingleNode("td[6]").InnerHtml, out value);
                goalie.Durability = value;

                int.TryParse(goalieNode.SelectSingleNode("td[7]").InnerHtml, out value);
                goalie.Endurance = value;

                int.TryParse(goalieNode.SelectSingleNode("td[8]").InnerHtml, out value);
                goalie.Size = value;

                int.TryParse(goalieNode.SelectSingleNode("td[9]").InnerHtml, out value);
                goalie.Agility = value;
                
                int.TryParse(goalieNode.SelectSingleNode("td[10]").InnerHtml, out value);
                goalie.Rebound = value;

                int.TryParse(goalieNode.SelectSingleNode("td[11]").InnerHtml, out value);
                goalie.StyleControl = value;

                int.TryParse(goalieNode.SelectSingleNode("td[12]").InnerHtml, out value);
                goalie.HandSpeed = value;

                int.TryParse(goalieNode.SelectSingleNode("td[13]").InnerHtml, out value);
                goalie.ReactionTime = value;

                int.TryParse(goalieNode.SelectSingleNode("td[14]").InnerHtml, out value);
                goalie.PuckHandling = value;

                int.TryParse(goalieNode.SelectSingleNode("td[15]").InnerHtml, out value);
                goalie.Passing = value;

                int.TryParse(goalieNode.SelectSingleNode("td[16]").InnerHtml, out value);
                goalie.Leadership = value;

                int.TryParse(goalieNode.SelectSingleNode("td[17]").InnerHtml, out value);
                goalie.Morale = value;

                int.TryParse(goalieNode.SelectSingleNode("td[18]").InnerHtml, out value);
                goalie.TA = value;

                int.TryParse(goalieNode.SelectSingleNode("td[19]").InnerHtml, out value);
                goalie.SP = value;

                int.TryParse(goalieNode.SelectSingleNode("td[20]").InnerHtml, out value);
                goalie.Age = value;

                int.TryParse(goalieNode.SelectSingleNode("td[21]").InnerHtml, out value);
                goalie.Contract = value;

                goalie.Salary = goalieNode.SelectSingleNode("td[22]").InnerHtml;

                players.Add(goalie);
            }
            
            response.Close();
        }
    }
}
