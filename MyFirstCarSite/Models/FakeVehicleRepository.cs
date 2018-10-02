using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstCarSite.Models
{
    public class FakeVehicleRepository : IVehicleRepository
    {
        public IQueryable<Vehicle> Vehicles => new List<Vehicle>
        {
            new Vehicle { Make = "Toyota", Price = 1000 },
            new Vehicle { Make = "Nissan", Price = 1100},
            new Vehicle { Make = "Skoda", Price = 950 }
        }.AsQueryable<Vehicle>();
    }
}
