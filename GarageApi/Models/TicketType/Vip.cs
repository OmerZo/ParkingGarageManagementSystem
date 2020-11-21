using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarageApi.Models
{
    public class Vip : ITicket
    {
        public int FirstLot { get; set; } = 1;
        public int LastLot { get; set; } = 10;
        public int Height { get; set; } = 999999; //No Limit
        public int Width { get; set; } = 999999; //No Limit
        public int Length { get; set; } = 999999; //No Limit
        public bool ClassA { get; set; } = true;
        public bool ClassB { get; set; } = true;
        public bool ClassC { get; set; } = true;
        public int Cost { get; set; } = 200;
        public int TimeLimit { get; set; } = 99999; //No Limit
    }
}