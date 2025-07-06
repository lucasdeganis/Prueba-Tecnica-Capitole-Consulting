using System;
using GtMotive.Estimate.Microservice.Domain.Enumerators;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Esta clase representa un alquiler de vehículo en el sistema de gestión de flotas.
    /// </summary>
    public class Rent
    {
        /// <summary>
        /// Gets or sets identificador unico de un alquiler.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets el identificador del vehículo alquilado.
        /// </summary>
        public Guid VehicleId { get; set; } // Foreign key to Vehicle

        /// <summary>
        /// Gets or sets el vehículo alquilado.
        /// </summary>
        public Vehicle Vehicle { get; set; } // Navigation property to Vehicle entity

        /// <summary>
        /// Gets or sets el identificador del cliente que alquila el vehículo.
        /// </summary>
        public Guid ClientId { get; set; } // Foreign key to User

        /// <summary>
        /// Gets or sets el cliente que alquila el vehículo.
        /// </summary>
        public Client Client { get; set; } // Navigation property to User entity

        /// <summary>
        /// Gets or sets la fecha de inicio del alquiler.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets la fecha de finalización del alquiler.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets el costo total del alquiler.
        /// </summary>
        public decimal TotalCost { get; set; } // Total cost of the rent

        /// <summary>
        /// Gets or sets el estado del alquiler.
        /// </summary>
        public RentStatus Status { get; set; } // e.g., Active, Completed, Cancelled

        /// <summary>
        /// Gets or sets la fecha y hora de recogida del vehículo.
        /// </summary>
        public int ActualKilometers { get; set; } // Actual kilometers driven during the rent

        /// <summary>
        /// Gets or sets la fecha y hora de entrega del vehículo.
        /// </summary>
        public int KilometersDriven { get; set; } // Kilometers driven during the rent

        /// <summary>
        /// Gets or sets el número de combustible al inicio del alquiler.
        /// </summary>
        public string Comments { get; set; } // Additional comments or notes about the rent

        /// <summary>
        /// Gets or sets la fecha de creación del alquiler.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
