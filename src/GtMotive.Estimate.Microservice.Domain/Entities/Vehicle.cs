using System;
using GtMotive.Estimate.Microservice.Domain.Enumerators;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Esta clase representa un vehículo en el sistema de gestión de flotas.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Gets or sets identificador único del vehículo.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets el estado del vehículo. e.g., Available, Rented, Under Maintenance, Unavailable.
        /// </summary>
        public VehicleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets la marca del vehículo.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets el modelo del vehículo.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets el año del vehículo.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets el VIN (Número de Identificación del Vehículo).
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// Gets or sets la matrícula del vehículo.
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Gets or sets el color del vehículo.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets el tipo de vehículo. e.g., Sedan, SUV, Truck.
        /// </summary>
        public VehicleType Type { get; set; }

        /// <summary>
        /// Gets or sets el kilometraje del vehículo.
        /// </summary>
        public int Kilometer { get; set; }

        /// <summary>
        /// Gets or sets la transmisión del vehículo. e.g., Automatic, Manual.
        /// </summary>
        public VehicleTransmission Transmission { get; set; }

        /// <summary>
        /// Gets or sets el número de asientos del vehículo.
        /// </summary>
        public int Seats { get; set; }

        /// <summary>
        /// Gets or sets el tipo de combustible del vehículo. e.g., Petrol, Diesel, Electric.
        /// </summary>
        public VehicleFuelType FuelType { get; set; }

        /// <summary>
        /// Gets or sets el número de puertas del vehículo.
        /// </summary>
        public int Doors { get; set; }

        /// <summary>
        /// Gets or sets la potencia del motor del vehículo.
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// Gets or sets la flota a la que pertenece el vehículo.
        /// </summary>
        public Fleet Fleet { get; set; }

        /// <summary>
        /// Gets or sets el identificador de la flota a la que pertenece el vehículo.
        /// </summary>
        public int PricePerHour { get; set; }

        /// <summary>
        /// gets or sets fecha de creación del vehículo.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
