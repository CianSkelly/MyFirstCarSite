using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFirstCarSite.Models;

namespace MyFirstCarSite.Models.ViewModels
{
    public class VehiclesListViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        //test for git push
        //2nd test for git changes
    }
}
