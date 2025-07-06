using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Host.Configuration
{
    internal static class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var fleetRepo = scope.ServiceProvider.GetRequiredService<IFleetRepository>();
            var vehicleRepo = scope.ServiceProvider.GetRequiredService<IVehicleRepository>();
            var clientRepo = scope.ServiceProvider.GetRequiredService<IClientRepository>();

            // Agregar una flota si no existe
            var flets = await fleetRepo.GetAllAsync();
            if (flets.Count == 0)
            {
                await fleetRepo.CreateAsync(new Fleet
                {
                    Id = Guid.NewGuid(),
                    Name = "Flota Principal Fast car rent",
                    Description = "Flota principal de la empresa Fast car rent",
                    Type = "Comercial",
                    ResponsiblePerson = "Carlos López",
                    Empresa = "Fast car rent",
                    CreatedAt = DateTime.UtcNow
                });

                await fleetRepo.CreateAsync(new Fleet
                {
                    Id = Guid.NewGuid(),
                    Name = "Flota Principal Rent now",
                    Description = "Flota principal de la empresa Rent now",
                    Type = "Comercial",
                    ResponsiblePerson = "Pamela Fernandez",
                    Empresa = "Rent now",
                    CreatedAt = DateTime.UtcNow
                });
            }

            // Agregar un vehículo si no existe
            var vehicles = await vehicleRepo.GetAllAsync();
            if (vehicles.Count == 0)
            {
                await vehicleRepo.CreateAsync(new Vehicle
                {
                    Id = Guid.NewGuid(),
                    Brand = "Toyota",
                    Model = "Corolla",
                    Year = 2025,
                    Status = Domain.Enumerators.VehicleStatus.Available,
                    VIN = "1234567890",
                    LicensePlate = "ABC123",
                    Color = "Blanco",
                    Type = Domain.Enumerators.VehicleType.Sedan,
                    Kilometer = 10000,
                    Transmission = Domain.Enumerators.VehicleTransmission.Automatic,
                    Seats = 5,
                    FuelType = Domain.Enumerators.VehicleFuelType.Electric,
                    Doors = 4,
                    Power = 120,
                    PricePerHour = 10,
                    CreatedAt = DateTime.UtcNow
                });

                await vehicleRepo.CreateAsync(new Vehicle
                {
                    Id = Guid.NewGuid(),
                    Brand = "BMW",
                    Model = "M3",
                    Year = 2024,
                    Status = Domain.Enumerators.VehicleStatus.Available,
                    VIN = "0987654321",
                    LicensePlate = "ABC231",
                    Color = "Azul",
                    Type = Domain.Enumerators.VehicleType.Sedan,
                    Kilometer = 1333,
                    Transmission = Domain.Enumerators.VehicleTransmission.Automatic,
                    Seats = 5,
                    FuelType = Domain.Enumerators.VehicleFuelType.Petrol,
                    Doors = 4,
                    Power = 390,
                    PricePerHour = 18,
                    CreatedAt = DateTime.UtcNow
                });

                await vehicleRepo.CreateAsync(new Vehicle
                {
                    Id = Guid.NewGuid(),
                    Brand = "Ford",
                    Model = "Mustang",
                    Year = 2018,
                    Status = Domain.Enumerators.VehicleStatus.Available,
                    VIN = "6789012345",
                    LicensePlate = "ZSQ221",
                    Color = "Blanco",
                    Type = Domain.Enumerators.VehicleType.Sport,
                    Kilometer = 112333,
                    Transmission = Domain.Enumerators.VehicleTransmission.Manual,
                    Seats = 4,
                    FuelType = Domain.Enumerators.VehicleFuelType.Petrol,
                    Doors = 2,
                    Power = 450,
                    PricePerHour = 25,
                    CreatedAt = DateTime.UtcNow
                });
            }

            // Agregar un cliente si no existe
            var clients = await clientRepo.GetAllAsync();
            if (clients.Count == 0)
            {
                await clientRepo.CreateAsync(new Client
                {
                    Id = Guid.Parse("1299d905-7da0-40c3-8b40-ee458e719e58"),
                    FirstName = "Juan",
                    LastName = "Pérez",
                    DocumentNumber = "12345678",
                    DocumentType = "DNI",
                    Email = "juan.perez@email.com",
                    PhoneNumber = "123456789",
                    Address = "Calle Falsa 123",
                    LicenseNumber = "LIC123",
                    CreatedAt = DateTime.UtcNow
                });

                await clientRepo.CreateAsync(new Client
                {
                    Id = Guid.Parse("ac232555-86ac-45db-b18a-357034ce30d6"),
                    FirstName = "Andrea",
                    LastName = "Cruz",
                    DocumentNumber = "23456123",
                    DocumentType = "DNI",
                    Email = "andrea.cruz@email.com",
                    Address = "Calle Alcala 123",
                    LicenseNumber = "LIC222",
                    CreatedAt = DateTime.UtcNow
                });

                await clientRepo.CreateAsync(new Client
                {
                    Id = Guid.Parse("7117d659-5c44-4afc-834d-47393ea034b1"),
                    FirstName = "Roberto",
                    LastName = "Godoy",
                    DocumentNumber = "33356123",
                    DocumentType = "DNI",
                    Email = "roberto.godoy@email.com",
                    Address = "Calle Ascuenaga 123",
                    LicenseNumber = "LIC333",
                    CreatedAt = DateTime.UtcNow
                });
            }
        }
    }
}
