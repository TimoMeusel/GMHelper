using System.Collections.Generic;
using System.Linq;

namespace GM.Model
{
    public class Skater : Player
    {
        public Skater (Team team, Dictionary<string, string> values) : base(team, values)
        {
            Name = values["Player Name"];
            IsCenter = !string.IsNullOrWhiteSpace(values["C"]);
            IsLeftWinger = !string.IsNullOrWhiteSpace(values["L"]);
            IsRightWinger = !string.IsNullOrWhiteSpace(values["R"]);
            IsDefender = !string.IsNullOrWhiteSpace(values["D"]);

            double condition;
            double.TryParse(values["CON"], out condition);
            Condition = condition;

            IsInjured = !string.IsNullOrWhiteSpace(values["IJ"]);

            int checking;
            int.TryParse(values["CK"], out checking);
            Checking = checking;

            int fighing;
            int.TryParse(values["FG"], out fighing);
            Fighting = fighing;

            int discipline;
            int.TryParse(values["DI"], out discipline);
            Discipline = discipline;

            int skating;
            int.TryParse(values["SK"], out skating);
            Skating = skating;

            int strength;
            int.TryParse(values["ST"], out strength);
            Strength = strength;

            int endurance;
            int.TryParse(values["EN"], out endurance);
            Endurance = endurance;

            int durablity;
            int.TryParse(values["DU"], out durablity);
            Durability = durablity;

            int puckHandling;
            int.TryParse(values["PH"], out puckHandling);
            PuckHandling = puckHandling;

            int faceOff;
            int.TryParse(values["FO"], out faceOff);
            FaceOff = faceOff;

            int passing;
            int.TryParse(values["PA"], out passing);
            Passing = passing;

            int scoring;
            int.TryParse(values["SC"], out scoring);
            Scoring = scoring;

            int defense;
            int.TryParse(values["DF"], out defense);
            Defense = defense;

            int penaltyShot;
            int.TryParse(values["PS"], out penaltyShot);
            PenaltyShot = penaltyShot;

            int experience;
            int.TryParse(values["EX"], out experience);
            Experience = experience;

            int leadership;
            int.TryParse(values["LD"], out leadership);
            Leadership = leadership;

            int mooale;
            int.TryParse(values["MO"], out mooale);
            Morale = mooale;

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
        }

        /// <summary>
        ///     C
        /// </summary>
        public bool IsCenter { get; internal set; }

        /// <summary>
        ///     LW
        /// </summary>
        public bool IsLeftWinger { get; internal set; }

        /// <summary>
        ///     RW
        /// </summary>
        public bool IsRightWinger { get; internal set; }

        /// <summary>
        ///     D
        /// </summary>
        public bool IsDefender { get; internal set; }

        /// <summary>
        ///     CK
        /// </summary>
        public int Checking { get; internal set; }

        /// <summary>
        ///     FG
        /// </summary>
        public int Fighting { get; internal set; }

        /// <summary>
        ///     DI
        /// </summary>
        public int Discipline { get; internal set; }

        /// <summary>
        ///     FO
        /// </summary>
        public int FaceOff { get; internal set; }

        /// <summary>
        ///     SC
        /// </summary>
        public int Scoring { get; internal set; }

        /// <summary>
        ///     DF
        /// </summary>
        public int Defense { get; internal set; }

        /// <summary>
        ///     PA
        /// </summary>
        public int Passing { get; internal set; }

        /// <summary>
        ///     EX
        /// </summary>
        public int Experience { get; internal set; }
    }
}
