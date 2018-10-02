using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MyFirstCarSite.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Vehicles.Any())
            {
                context.Vehicles.AddRange
                    (
                        new Vehicle
                        {
                            Colour = "Red",
                            Make = "Toyota",
                            Price = 950,
                            VehicleModel = "Starlet",
                            VehicleCategory = "Toyota"
                        },
                        new Vehicle
                        {
                            Colour = "Red",
                            Make = "Toyota",
                            Price = 950,
                            VehicleModel = "Yaris",
                            VehicleCategory = "Toyota"
                        },
                        new Vehicle
                        {
                            Colour = "Grey",
                            Make = "Toyota",
                            Price = 850,
                            VehicleModel = "Starlet",
                            VehicleCategory = "Toyota"
                        },
                        new Vehicle
                        {
                            Colour = "Blue",
                            Make = "Toyota",
                            Price = 1050,
                            VehicleModel = "Yaris",
                            VehicleCategory = "Toyota"
                        },
                        new Vehicle
                        {
                            Colour = "Green",
                            Make = "Nissan",
                            Price = 1000,
                            VehicleModel = "Micra",
                            VehicleCategory = "Nissan"
                        },
                        new Vehicle
                        {
                            Colour = "Red",
                            Make = "Nissan",
                            Price = 1050,
                            VehicleModel = "Micra",
                            VehicleCategory = "Nissan"
                        },
                        new Vehicle
                        {
                            Colour = "Purple",
                            Make = "Nissan",
                            Price = 1250,
                            VehicleModel = "March",
                            VehicleCategory = "Nissan"
                        },
                        new Vehicle
                        {
                            Colour = "Green",
                            Make = "Volkswagen",
                            Price = 1250,
                            VehicleModel = "Polo",
                            VehicleCategory = "Volkswagen"
                        },
                        new Vehicle
                        {
                            Colour = "Grey",
                            Make = "Skoda",
                            Price = 725,
                            VehicleModel = "Fabia",
                            VehicleCategory = "Skoda"
                        },
                        new Vehicle
                        {
                            Colour = "Red",
                            Make = "Mazda",
                            Price = 450,
                            VehicleModel = "121",
                            VehicleCategory = "Mazda"
                        }

                       );
                context.SaveChanges();
            }
        }
    }
}
