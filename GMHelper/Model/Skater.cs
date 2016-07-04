namespace GM.Model
{
    public class Skater : Player
    {
        public bool IsCenter { get; internal set; }
        public bool IsLeftWinger { get; internal set; }
        public bool IsRightWinger { get; internal set; }
        public bool IsDefender { get; internal set; }
        public int Checking { get; internal set; }
        public int Fighting { get; internal set; }
        public int Discipline { get; internal set; }
        public int FaceOff { get; internal set; }
        public int Scoring { get; internal set; }
        public int Defense { get; internal set; }
        public int Passing { get; internal set; }
        public int Experience { get; internal set; }
    }
}
