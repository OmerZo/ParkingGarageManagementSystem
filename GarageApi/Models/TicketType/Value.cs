using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarageApi.Models
{
    public class Value : ITicket
    {
        public int FirstLot { get; set; } = 11;
        public int LastLot { get; set; } = 30;
        public int Height { get; set; } = 2500;
        public int Width { get; set; } = 2400;
        public int Length { get; set; } = 5000;
        public bool ClassA { get; set; } = true;
        public bool ClassB { get; set; } = true;
        public bool ClassC { get; set; } = false;
        public int Cost { get; set; } = 100;
        public int TimeLimit { get; set; } = 72;
    }
}