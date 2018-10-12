using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using MyFirstCarSite.Controllers;
using MyFirstCarSite.Models;
using Xunit;
using System.Text;
using MyFirstCarSite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstCarSite.Tests
{
    public class VehicleControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IVehicleRepository> mock = new Mock<IVehicleRepository>();
            mock.Setup(m => m.Vehicles).Returns((new Vehicle[]
            {
                new Vehicle { VehicleID = 1, Make = "V1"},
                new Vehicle { VehicleID = 2, Make = "V2"},
                new Vehicle { VehicleID = 3, Make = "V3"},
                new Vehicle { VehicleID = 4, Make = "V4"},
                new Vehicle { VehicleID = 5, Make = "V5"}
            }).AsQueryable<Vehicle>());

            VehicleController controller = new VehicleController(mock.Object);
            controller.PageSize = 3;

            //Act
            VehiclesListViewModel result = controller.List(null, 2).ViewData.Model as VehiclesListViewModel;

            //Assert
            Vehicle[] vehicleArray = result.Vehicles.ToArray();
            Assert.True(vehicleArray.Length == 2);
            Assert.Equal("V4", vehicleArray[0].Make);
            Assert.Equal("V5", vehicleArray[1].Make);

        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            Mock<IVehicleRepository> mock = new Mock<IVehicleRepository>();
            mock.Setup(m => m.Vehicles).Returns((new Vehicle[]
            {
                new Vehicle { VehicleID = 1, Make = "V1"},
                new Vehicle { VehicleID = 2, Make = "V2"},
                new Vehicle { VehicleID = 3, Make = "V3"},
                new Vehicle { VehicleID = 4, Make = "V4"},
                new Vehicle { VehicleID = 5, Make = "V5"}
            }).AsQueryable<Vehicle>());

            //Arrange
            VehicleController controller = new VehicleController(mock.Object) { PageSize = 3 };

            //Act
            VehiclesListViewModel result =
                controller.List(null, 2).ViewData.Model as VehiclesListViewModel;

            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Vehicles()
        {
            //Arrange
            // - create the mock repository
            Mock<IVehicleRepository> mock = new Mock<IVehicleRepository>();
            mock.Setup(m => m.Vehicles).Returns((new Vehicle[]{
                new Vehicle { VehicleID = 1, Make = "V1", VehicleCategory = "Cat1"},
                new Vehicle { VehicleID = 2, Make = "V2", VehicleCategory = "Cat2"},
                new Vehicle { VehicleID = 3, Make = "V3", VehicleCategory = "Cat1"},
                new Vehicle { VehicleID = 4, Make = "V4", VehicleCategory = "Cat2"},
                new Vehicle { VehicleID = 5, Make = "V5", VehicleCategory = "Cat3"}
            }).AsQueryable<Vehicle>());

            //Arrange - create a controller and make the page size 3 items
            VehicleController controller = new VehicleController(mock.Object);
            controller.PageSize = 3;

            //Action
            Vehicle[] result =
                (controller.List("Cat2", 1).ViewData.Model as VehiclesListViewModel).Vehicles.ToArray();

            //Assert
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Make == "V2" && result[0].VehicleCategory == "Cat2");
            Assert.True(result[1].Make == "V4" && result[1].VehicleCategory == "Cat2");
        }

        [Fact]
        public void Generate_Category_Specific_Vehicle_Count()
        {
            //Arrange
            Mock<IVehicleRepository> mock = new Mock<IVehicleRepository>();
            mock.Setup(m => m.Vehicles).Returns((new Vehicle[]
            {
                new Vehicle { VehicleID = 1, Make = "V1", VehicleCategory = "Cat1"},
                new Vehicle { VehicleID = 2, Make = "V2", VehicleCategory = "Cat2"},
                new Vehicle { VehicleID = 3, Make = "V3", VehicleCategory = "Cat1"},
                new Vehicle { VehicleID = 4, Make = "V4", VehicleCategory = "Cat2"},
                new Vehicle { VehicleID = 5, Make = "V5", VehicleCategory = "Cat3"}
            }).AsQueryable<Vehicle>());

            
            VehicleController target = new VehicleController(mock.Object);
            target.PageSize = 3;

            Func<ViewResult, VehiclesListViewModel> GetModel = result => result?.ViewData?.Model as VehiclesListViewModel;

            //Action
            int? res1 = GetModel(target.List("Cat1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(target.List("Cat2"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(target.List("Cat3"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(target.List(null))?.PagingInfo.TotalItems;

            //Assert
            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}
