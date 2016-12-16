using System;

namespace GM.Model
{
    internal class PlayerIdentifier
    {
        public PlayerIdentifier (string name, DateTime birthday)
        {
            Name = name;
            Birthday = birthday;
        }

        public string Name { get; private set; }
        public DateTime Birthday { get; private set; }
    }
}
