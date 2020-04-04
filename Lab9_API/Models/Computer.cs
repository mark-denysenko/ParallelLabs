using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab9_API.Models
{
    public class Computer
    {
        public string Name { get; set; }
        public float CPU { get; set; }
        public int RAM { get; set; }
        public bool IsDiscreteVideoCard { get; set; }
    }
}
