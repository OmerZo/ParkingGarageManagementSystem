using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarageApi.Models.Vehicles
{
    public class Vehicle
    {
        public string LicenseId { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public string VehicleType { get; set; }
        public char Class { get; set; }


        public void ChooseClass()
        {
            switch(VehicleType.ToLower().Trim())
            {
                case "motorcycle":
                case "private":
                case "crossover":
                    Class = 'A';
                    break;
                case "suv":
                case "van":
                    Class = 'B';
                    break;
                case "truck":
                    Class = 'C';
                    break;
            }
        }
    }
}