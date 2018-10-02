using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstCarSite.Models
{
    public interface IVehicleRepository
    {
        IQueryable<Vehicle> Vehicles { get; }
    }
}
