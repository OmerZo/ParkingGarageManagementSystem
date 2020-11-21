using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarageApi.Models
{
    public class Regular : ITicket
    {
        public int FirstLot { get; set; } = 31;
        public int LastLot { get; set; } = 60;
        public int Height { get; set; } = 2000;
        public int Width { get; set; } = 2000;
        public int Length { get; set; } = 3000;
        public bool ClassA { get; set; } = true;
        public bool ClassB { get; set; } = false;
        public bool ClassC { get; set; } = false;
        public int Cost { get; set; } = 50;
        public int TimeLimit { get; set; } = 24;
    }
}