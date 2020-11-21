using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApi.Models
{
    public interface ITicket
    {
        int FirstLot { get; set; }
        int LastLot { get; set; }
        int Height { get; set; }
        int Width { get; set; }
        int Length { get; set; }
        bool ClassA { get; set; }
        bool ClassB { get; set; }
        bool ClassC { get; set; }
        int Cost { get; set; }
        int TimeLimit { get; set; }
    }
}
