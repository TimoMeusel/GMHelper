using System.Collections.Generic;
using GM.Model;

namespace GM.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        private readonly Player _player;

        public PlayerViewModel (Player player)
        {
            _player = player;
        }
        
        public Dictionary<string, string> Values => _player.Values;

        /// <summary>   
        ///     The player's name
        /// </summary>
        public string Name => _player.Name;

        /// <summary>
        ///     The player's team
        /// </summary>
        public Team Team => _player.Team;

        /// <summary>
        ///     CON
        /// </summary>
        public double Condition => _player.Condition;

        /// <summary>
        ///     IJ
        /// </summary>
        public bool IsInjured => _player.IsInjured;

        /// <summary>
        ///     SK
        /// </summary>
        public int Skating => _player.Skating;

        /// <summary>
        ///     ST
        /// </summary>
        public int Strength => _player.Strength;

        /// <summary>
        ///     EN
        /// </summary>
        public int Endurance => _player.Endurance;

        /// <summary>
        ///     PH
        /// </summary>
        public int PuckHandling => _player.PuckHandling;

        /// <summary>
        ///     PS
        /// </summary>
        public int PenaltyShot => _player.PenaltyShot;

        /// <summary>
        ///     DU
        /// </summary>
        public int Durability => _player.Durability;

        /// <summary>
        ///     LD
        /// </summary>
        public int Leadership => _player.Leadership;

        /// <summary>
        ///     MO
        /// </summary>
        public int Morale => _player.Morale;

        /// <summary>
        ///     Todo: Unknown
        /// </summary>
        public int TA => _player.TA;

        /// <summary>
        ///     Todo: Unknown
        /// </summary>
        public int SP => _player.SP;

        /// <summary>
        ///     The palyer's age
        /// </summary>
        public int Age => _player.Age;

        /// <summary>
        ///     The contract duration
        /// </summary>
        public int Contract => _player.Contract;

        /// <summary>
        ///     The yearly salary
        /// </summary>
        public int Salary => _player.Salary;
    }
}