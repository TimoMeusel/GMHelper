using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GM.Model
{
    /// <summary>
    ///     An abstract general representation of a player. Can be either <see cref="Goalie"/> or <see cref="Player"/>
    /// </summary>
    [DebuggerDisplay("{Name} - {Franchise.Name}")]
    public class AdditionalPlayerInfo
    {
        public AdditionalPlayerInfo(Team team, Dictionary<string, string> values)
        {
            Franchise = team;
            Name = values["Player Name"];
            Country = values["CNT"];
            Birthday = DateTime.Parse(values["Birthday"]);
            Weight = values["Weight"];
            Height = values["Height"];
        }   

        /// <summary>
        ///     The player's name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        ///     The player's team
        /// </summary>
        public Team Franchise { get; protected set; }

        /// <summary>
        ///     The country of birth
        /// </summary>
        public string Country { get; protected set; }

        /// <summary>
        ///     The birthday
        /// </summary>
        public DateTime Birthday { get; protected set; }

        /// <summary>
        ///     The Weight
        /// </summary>
        public string Weight { get; protected set; }

        /// <summary>
        ///     The Height
        /// </summary>
        public string Height { get; protected set; }
    }
}
