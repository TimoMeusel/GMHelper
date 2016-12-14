using System;
using System.Collections.Generic;
using System.Linq;

namespace GM.Model
{
    /// <summary>
    ///     Repersents a Goalie
    /// </summary>
    public class Goalie : Player
    {
        /// <summary>
        ///     Creates a goalie representation based on a ordered list of values
        /// </summary>
        /// <param name="values"></param>
        public Goalie (Team team, Dictionary<string, string> values):base(team, values)
        {
            Name = values["Goalie Name"];
            
            double condition;
            double.TryParse(values["CON"], out condition);
            Condition = condition;

            IsInjured = !string.IsNullOrWhiteSpace(values["IJ"]);

            int skating;
            int.TryParse(values["SK"], out skating);
            Skating = skating;

            int durability;
            int.TryParse(values["DU"], out durability);
            Durability = durability;

            int endurance;
            int.TryParse(values["EN"], out endurance);
            Endurance = endurance;

            int size;
            int.TryParse(values["SZ"], out size);
            Size = size;

            int agility;
            int.TryParse(values["AG"], out agility);
            Agility = agility;

            int rebound;
            int.TryParse(values["RB"], out rebound);
            Rebound = rebound;

            int styleControl;
            int.TryParse(values["SC"], out styleControl);
            StyleControl = styleControl;

            int handSpeed;
            int.TryParse(values["HS"], out handSpeed);
            HandSpeed = handSpeed;

            int reactionTime;
            int.TryParse(values["RT"], out reactionTime);
            ReactionTime = reactionTime;

            int puckHandling;
            int.TryParse(values["PH"], out puckHandling);
            PuckHandling = puckHandling;

            int penaltyShot;
            int.TryParse(values["PS"], out penaltyShot);
            PenaltyShot = penaltyShot;

            int leaderhip;
            int.TryParse(values["LD"], out leaderhip);
            Leadership = leaderhip;

            int morale;
            int.TryParse(values["MO"], out morale);
            Morale = morale;

            int ta;
            int.TryParse(values["TA"], out ta);
            TA = ta;

            int sp;
            int.TryParse(values["SP"], out sp);
            SP = sp;

            int age;
            int.TryParse(values["Age"], out age);
            Age = age;

            int contract;
            int.TryParse(values["Contract"], out contract);
            Contract = contract;

            string salary = values["Salary"];
            string salaryText = salary.Split(' ').First().Replace(".", "");
            int salaryValue;
            int.TryParse(salaryText, out salaryValue);
            Salary = salaryValue;

            string country;
            values.TryGetValue("Country", out country);
            Country = country;

            string height;
            values.TryGetValue("Height", out height);
            Height = height;

            string weight;
            values.TryGetValue("Weight", out weight);
            Weight = weight;

            string birthdayText;
            values.TryGetValue("Birthday", out birthdayText);
            DateTime birthday;
            DateTime.TryParse(birthdayText, out birthday);
            Birthday = birthday;

            string eliteProspectsId;
            values.TryGetValue("EliteProspectsId", out eliteProspectsId);
            EliteProspectsId = eliteProspectsId;
        }

        /// <summary>
        ///     SZ
        /// </summary>
        public int Size { get; internal set; }

        /// <summary>
        ///     AG
        /// </summary>
        public int Agility { get; internal set; }

        /// <summary>
        ///     RB
        /// </summary>
        public int Rebound { get; internal set; }

        /// <summary>
        ///     SC
        /// </summary>
        public int StyleControl { get; internal set; }

        /// <summary>
        ///     HS
        /// </summary>
        public int HandSpeed { get; internal set; }

        /// <summary>
        ///     RT
        /// </summary>
        public int ReactionTime { get; internal set; }
    }
}
