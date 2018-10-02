using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstCarSite.Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        public string Make { get; set; }
        public string VehicleModel { get; set; }
        public string Colour { get; set; }
        public decimal Price { get; set; }
        public string VehicleCategory { get; set; }

    }
}
