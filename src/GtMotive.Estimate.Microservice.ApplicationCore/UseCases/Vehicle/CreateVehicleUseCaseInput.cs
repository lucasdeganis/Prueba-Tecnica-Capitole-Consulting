using System;
using System.Text.Json.Serialization;
using GtMotive.Estimate.Microservice.Domain.Enumerators;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle
{
    /// <summary>
    /// Este caso de uso se encarga de crear un nuevo vehículo en el sistema de gestión de flotas.
    /// </summary>
    public class CreateVehicleUseCaseInput : IUseCaseInput
    {
        /// <summary>
        /// Gets or sets marca del vehículo.
        /// </summary>
        [JsonRequired]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets modelo del vehículo.
        /// </summary>
        [JsonRequired]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets año del vehículo.
        /// </summary>
        [JsonRequired]
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets VIN (Número de Identificación del Vehículo).
        /// </summary>
        [JsonRequired]
        public string VIN { get; set; }

        /// <summary>
        /// Gets or sets matrícula del vehículo.
        /// </summary>
        [JsonRequired]
        public string LicensePlate { get; set; }

        /// <summary>
        /// Gets or sets color del vehículo.
        /// </summary>
        [JsonRequired]
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets el tipo de vehículo. e.g., Sedan, SUV, Truck.
        /// </summary>
        [JsonRequired]
        public VehicleType Type { get; set; }

        /// <summary>
        /// Gets or sets kilometraje del vehículo.
        /// </summary>
        [JsonRequired]
        public int Kilometer { get; set; }

        /// <summary>
        /// Gets or sets tipo de transmisión del vehículo. e.g., Automatic, Manual.
        /// </summary>
        [JsonRequired]
        public VehicleTransmission Transmission { get; set; }

        /// <summary>
        /// Gets or sets numero de asientos del vehículo.
        /// </summary>
        [JsonRequired]
        public int Seats { get; set; }

        /// <summary>
        /// Gets or sets tipo de combustible del vehículo. e.g., Petrol, Diesel, Electric.
        /// </summary>
        [JsonRequired]
        public VehicleFuelType FuelType { get; set; }

        /// <summary>
        /// Gets or sets numero de puertas del vehículo.
        /// </summary>
        [JsonRequired]
        public int Doors { get; set; }

        /// <summary>
        /// Gets or sets potencia del vehículo en caballos de fuerza.
        /// </summary>
        [JsonRequired]
        public int Power { get; set; }

        /// <summary>
        /// Gets or sets el identificador único de la flota a la que pertenece el vehículo.
        /// </summary>
        public Guid FleetId { get; set; }
    }
}
