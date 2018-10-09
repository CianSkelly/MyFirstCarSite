using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFirstCarSite.Models;

namespace MyFirstCarSite.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IVehicleRepository repository;

        public NavigationMenuViewComponent(IVehicleRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedVehicleCategory = RouteData?.Values["vehicleCategory"];
            return View(repository.Vehicles
                .Select(x => x.VehicleCategory)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
