using System;
using GtMotive.Estimate.Microservice.Domain.Enumerators;

namespace GtMotive.Estimate.Microservice.Domain.Dto
{
    /// <summary>
    /// Esta clase representa un DTO (Data Transfer Object) para la entidad Vehicle.
    /// </summary>
    public class VehicleDto
    {
        /// <summary>
        /// Gets or sets identificador único del vehículo.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets el estado del vehículo.
        /// </summary>
        public VehicleStatus Status { get; set; } // e.g., Available, Rented, Under Maintenance, Unavailable

        /// <summary>
        /// gets or sets la marca del vehículo.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// gets or sets el modelo del vehículo.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// gets or sets el año del vehículo.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// gets or sets el VIN (Número de Identificación del Vehículo).
        /// </summary>
        public string VIN { get; set; } // Vehicle Identification Number

        /// <summary>
        /// gets or sets la matrícula del vehículo.
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// gets or sets el color del vehículo.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// gets or sets el tipo de vehículo.
        /// </summary>
        public VehicleType Type { get; set; } // e.g., Sedan, SUV, Truck

        /// <summary>
        /// gets or sets el kilometraje del vehículo.
        /// </summary>
        public int Kilometer { get; set; }

        /// <summary>
        /// gets or sets la transmisión del vehículo.
        /// </summary>
        public VehicleTransmission Transmission { get; set; } // e.g., Automatic, Manual

        /// <summary>
        /// gets or sets el número de asientos del vehículo.
        /// </summary>
        public int Seats { get; set; } // Number of seats

        /// <summary>
        /// gets or sets el tipo de combustible del vehículo.
        /// </summary>
        public VehicleFuelType FuelType { get; set; } // e.g., Petrol, Diesel, Electric

        /// <summary>
        /// gets or sets el número de puertas del vehículo.
        /// </summary>
        public int Doors { get; set; } // Number of doors

        /// <summary>
        /// gets or sets la potencia del motor del vehículo.
        /// </summary>
        public int Power { get; set; } // Engine power in horsepower

        /// <summary>
        /// gets or sets la capacidad del maletero del vehículo.
        /// </summary>
        public FleetDto Fleet { get; set; } // Navigation property to Fleet entity

        /// <summary>
        /// gets or sets fecha de creación del vehículo.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
