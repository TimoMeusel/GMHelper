namespace GM.Model
{
    /// <summary>
    ///     Represents a farm team that belongs to a pro team
    /// </summary>
    public class FarmTeam: Team
    {
        /// <summary>
        ///     Creates a new instance passing the pro team
        /// </summary>
        public FarmTeam (string name, Team proTeam):base(name)
        {
            ProTeam = proTeam;
        }

        /// <summary>
        ///     The pro team to that farm team
        /// </summary>
        public Team ProTeam { get; }


        public override bool Equals(object obj)
        {
            var team = obj as FarmTeam;
            if ( team == null )
            {
                return false;
            }

            return Name == team.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}