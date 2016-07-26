using GM.Model;

namespace GM.ViewModel
{
    public class GoalieViewModel : PlayerViewModel
    {
        private readonly Goalie _goalie;

        public GoalieViewModel (Goalie goalie)
            : base(goalie)
        {
            _goalie = goalie;
        }

        /// <summary>
        ///     SZ
        /// </summary>
        public int Size => _goalie.Size;

        /// <summary>
        ///     AG
        /// </summary>
        public int Agility => _goalie.Agility;

        /// <summary>
        ///     RB
        /// </summary>
        public int Rebound => _goalie.Rebound;

        /// <summary>
        ///     SC
        /// </summary>
        public int StyleControl => _goalie.StyleControl;

        /// <summary>
        ///     HS
        /// </summary>
        public int HandSpeed => _goalie.HandSpeed;

        /// <summary>
        ///     RT
        /// </summary>
        public int ReactionTime => _goalie.ReactionTime;
    }
}