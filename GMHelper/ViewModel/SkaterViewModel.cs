using GM.Model;

namespace GM.ViewModel
{
    public class SkaterViewModel : PlayerViewModel
    {
        private readonly Skater _skater;

        public SkaterViewModel (Skater skater):base(skater)
        {
            _skater = skater;
        }

        /// <summary>
        ///     C
        /// </summary>
        public bool IsCenter => _skater.IsCenter;

        /// <summary>
        ///     LW
        /// </summary>
        public bool IsLeftWinger => _skater.IsLeftWinger;

        /// <summary>
        ///     RW
        /// </summary>
        public bool IsRightWinger => _skater.IsRightWinger;

        /// <summary>
        ///     D
        /// </summary>
        public bool IsDefender => _skater.IsDefender;

        /// <summary>
        ///     CK
        /// </summary>
        public int Checking => _skater.Checking;

        /// <summary>
        ///     FG
        /// </summary>
        public int Fighting => _skater.Fighting;

        /// <summary>
        ///     DI
        /// </summary>
        public int Discipline => _skater.Discipline;

        /// <summary>
        ///     FO
        /// </summary>
        public int FaceOff => _skater.FaceOff;

        /// <summary>
        ///     SC
        /// </summary>
        public int Scoring => _skater.Scoring;

        /// <summary>
        ///     DF
        /// </summary>
        public int Defense => _skater.Defense;

        /// <summary>
        ///     PA
        /// </summary>
        public int Passing => _skater.Passing;

        /// <summary>
        ///     EX
        /// </summary>
        public int Experience => _skater.Experience;
    }
}