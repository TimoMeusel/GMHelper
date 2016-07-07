using System.Collections.Generic;
using System.Diagnostics;

namespace GM.Model
{
    /// <summary>
    ///     An abstract general representation of a player. Can be either <see cref="Goalie"/> or <see cref="Player"/>
    /// </summary>
    [DebuggerDisplay("{Name} - {Team}")]
    public abstract class Player
    {
        public Player (Team team, Dictionary<string, string> values)
        {
            Team = team;
            Values = values;
        }

        public Dictionary<string, string> Values { get; protected set; }

        /// <summary>
        ///     The player's name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        ///     The player's team
        /// </summary>
        public Team Team { get; protected set; }

        /// <summary>
        ///     CON
        /// </summary>
        public double Condition { get; protected set; }

        /// <summary>
        ///     IJ
        /// </summary>
        public bool IsInjured { get; protected set; }

        /// <summary>
        ///     SK
        /// </summary>
        public int Skating { get; protected set; }

        /// <summary>
        ///     ST
        /// </summary>
        public int Strength { get; protected set; }

        /// <summary>
        ///     EN
        /// </summary>
        public int Endurance { get; protected set; }

        /// <summary>
        ///     PH
        /// </summary>
        public int PuckHandling { get; protected set; }

        /// <summary>
        ///     PS
        /// </summary>
        public int PenaltyShot { get; protected set; }

        /// <summary>
        ///     DU
        /// </summary>
        public int Durability { get; protected set; }

        /// <summary>
        ///     LD
        /// </summary>
        public int Leadership { get; protected set; }

        /// <summary>
        ///     MO
        /// </summary>
        public int Morale { get; protected set; }

        /// <summary>
        ///     Todo: Unknown
        /// </summary>
        public int TA { get; protected set; }

        /// <summary>
        ///     Todo: Unknown
        /// </summary>
        public int SP { get; protected set; }

        /// <summary>
        ///     The palyer's age
        /// </summary>
        public int Age { get; protected set; }

        /// <summary>
        ///     The contract duration
        /// </summary>
        public int Contract { get; protected set; }

        /// <summary>
        ///     The yearly salary
        /// </summary>
        public int Salary { get; protected set; }
    }
}
