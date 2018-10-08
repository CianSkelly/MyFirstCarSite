using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFirstCarSite.Models;
using MyFirstCarSite.Models.ViewModels;

namespace MyFirstCarSite.Controllers
{
    public class VehicleController : Controller
    {
        private IVehicleRepository repository;
        public int PageSize = 4;

        public VehicleController(IVehicleRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string make, int vehiclePage = 1) 
            => View(new VehiclesListViewModel
            {
                Vehicles = repository.Vehicles
                    .Where(v => make == null || v.Make == make)
                    .OrderBy(v => v.VehicleID)
                    .Skip((vehiclePage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = vehiclePage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Vehicles.Count()
                },
                CurrentCategory = make
            });
    }
}
