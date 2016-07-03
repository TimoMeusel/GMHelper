using System;
using System.Diagnostics;

namespace GM.Model
{
    [DebuggerDisplay("{Name} - {Team}")]
    public abstract class Player
    {
        public string Name { get; internal set; }
        public string Team { get; internal set; }
        public double Condition { get; internal set; }
        public bool IsInjured { get; internal set; }
        public int Skating { get; internal set; }
        public int Strength { get; internal set; }
        public int Endurance { get; internal set; }
        public int PuckHandling { get; internal set; }
        public int Passing { get; internal set; }
        public int Durability { get; internal set; }
        public int Leadership { get; internal set; }
        public int Morale { get; internal set; }
        public int TA { get; internal set; }
        public int SP { get; internal set; }
        public int Age { get; internal set; }
        public int Contract { get; internal set; }
        public Decimal Salary { get; internal set; }
    }
}
