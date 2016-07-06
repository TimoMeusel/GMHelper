namespace GM.Model
{

    /// <summary>
    /// Represents a general team
    /// </summary>
    public class Team
    {
        private static Team _noTeam;

        public Team (string name)
        {
            Name = name;
        }

        public static Team NoTeam
        {
            get
            {
                if (_noTeam == null)
                {
                    _noTeam = new Team("-");
                }
                return _noTeam;
            }
        }
        /// <summary>
        ///     The name of the team
        /// </summary>
        public string Name { get; }

        public override bool Equals (object obj)
        {
            var team = obj as Team;
            if (team == null)
            {
                return false;
            }

            return Name == team.Name;
        }

        public override int GetHashCode ()
        {
            return Name.GetHashCode();
        }
    }
}