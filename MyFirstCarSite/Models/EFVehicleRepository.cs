using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstCarSite.Models
{
    public class EFVehicleRepository : IVehicleRepository
    {
        private ApplicationDbContext context;
        public EFVehicleRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Vehicle> Vehicles => context.Vehicles;
    }
}
