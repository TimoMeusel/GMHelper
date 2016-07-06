namespace GM.Model
{

    /// <summary>
    /// Represents a Team
    /// </summary>
    public class Team
    {
        private static Team _noTeam;

        public Team (string name, bool isPro)
        {
            Name = name;
            IsPro = isPro;
        }

        public static Team NoTeam
        {
            get
            {
                if (_noTeam == null)
                {
                    _noTeam = new Team("-", false);
                }
                return _noTeam;
            }
        }
        /// <summary>
        ///     The name of the team
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Indicator whether the team is pro or farmteam
        /// </summary>
        public bool IsPro { get; }

        public override bool Equals (object obj)
        {
            var team = obj as Team;
            if (team == null)
            {
                return false;
            }

            return Name == team.Name && IsPro == team.IsPro;
        }

        public override int GetHashCode ()
        {
            return Name.GetHashCode() * IsPro.GetHashCode();
        }
    }
}