using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4.DiningPhilosophersProblem
{
    public class Fork
    {
        public readonly int Id;
        public bool IsTaken { get; set; }

        public Fork(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return $"Fork - {Id}";
        }
    }
}
