using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using MyFirstCarSite.Components;
using MyFirstCarSite.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace MyFirstCarSite.Tests
{
    public class NavigtionMenuViewComponentTests
    {

        [Fact]
        public void Can_Select_VehicleCategories()
        {
            //Arrange
            Mock<IVehicleRepository> mock = new Mock<IVehicleRepository>();
            mock.Setup(m => m.Vehicles).Returns((new Vehicle[]
            {
                new Vehicle {VehicleID = 1, Make = "V1", VehicleCategory = "Toyota"},
                new Vehicle {VehicleID = 2, Make = "V2", VehicleCategory = "Toyota"},
                new Vehicle {VehicleID = 3, Make = "V3", VehicleCategory = "Nissan"},
                new Vehicle {VehicleID = 4, Make = "V4", VehicleCategory = "Mazda"},

            }).AsQueryable<Vehicle>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            //Act = get the set of vehicle categories
            string[] results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData
                .Model).ToArray();

            //Assert
            Assert.True(Enumerable.SequenceEqual(new string[] { "Toyota", "Mazda", "Nissan" }, results));
        }

        [Fact]
        public void Indicates_Selected_VehicleCategory()
        {
            //Arrange
            string vehicleCategoryToSelect = "Toyota";
            Mock<IVehicleRepository> mock = new Mock<IVehicleRepository>();
            mock.Setup(m => m.Vehicles).Returns((new Vehicle[]
            {
                new Vehicle { VehicleID = 1, Make = "V1", VehicleCategory = "Toyota"},
                new Vehicle { VehicleID = 3, Make = "V1", VehicleCategory = "Nissan"},
            }).AsQueryable<Vehicle>());
            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = new RouteData()
                }
            };
            target.RouteData.Values["vehicleCategory"] = vehicleCategoryToSelect;

            //Action
            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedVehicleCategory"];

            //Assert
            Assert.Equal(vehicleCategoryToSelect, result);

        }


    }

}

