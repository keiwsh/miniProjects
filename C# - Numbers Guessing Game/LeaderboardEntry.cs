using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zahlenraten
{
    internal class LeaderboardEntry : IComparable<LeaderboardEntry>
    {
        public int Difficulty { get; set; }

        public string? Name { get; set; }

        public int Trials { get; set; }

        public int CompareTo(LeaderboardEntry? other)
        {
            if (other == null) return 0;
            if (this == other) return 0;    
            if (this.Trials < other.Trials) return -1;
            if (this.Trials > other.Trials) return 1;
            return 0;
        }
    }

}