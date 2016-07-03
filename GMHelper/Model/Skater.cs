namespace GM.Model
{
    internal class Skater : Player
    {
        public bool IsCenter { get; set; }
        public bool IsLeftWinger { get; set; }
        public bool IsRightWinger { get; set; }
        public bool IsDefender { get; set; }
        public int Checking { get; set; }
        public int Fighting { get; set; }
        public int Discipline { get; set; }
        public int FaceOff { get; set; }
        public int Scoring { get; set; }
        public int Defense { get; set; }
        public int PenaltyShot { get; set; }
        public int Experience { get; set; }
    }
}
