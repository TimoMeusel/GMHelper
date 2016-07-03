using System.Diagnostics;

namespace GM.Model
{
    [DebuggerDisplay("{Name} - {Team}")]
    internal abstract class Player
    {
        public string Name { get; set; }
        public string Team { get; set; }
        public double Condition { get; set; }
        public bool IsInjured { get; set; }
        public int Skating { get; set; }
        public int Strength { get; set; }
        public int Endurance { get; set; }
        public int PuckHandling { get; set; }
        public int Passing { get; set; }
        public int Durability { get; set; }
        public int Leadership { get; set; }
        public int Morale { get; set; }
        public int TA { get; set; }
        public int SP { get; set; }
        public int Age { get; set; }
        public int Contract { get; set; }
        public string Salary { get; set; }
    }
}
