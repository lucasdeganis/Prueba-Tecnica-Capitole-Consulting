using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rent
{
    /// <summary>
    /// Esta clase representa la entrada para el caso de uso de alquiler de vehículo.
    /// </summary>
    public class RentVehicleUseCaseInput : IUseCaseInput
    {
        /// <summary>
        /// Gets or sets el identificador único del vehículo que se está alquilando.
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets el identificador único del cliente que está alquilando el vehículo.
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Gets or sets la fecha de inicio del alquiler del vehículo.
        /// </summary>
        public DateTime RentStartDate { get; set; }

        /// <summary>
        /// Gets or sets la fecha de finalización del alquiler del vehículo.
        /// </summary>
        public DateTime RentEndDate { get; set; }

        /// <summary>
        /// Gets or sets el precio del alquiler.
        /// </summary>
        public decimal TotalCost { get; set; }

        /// <summary>
        /// Gets or sets commentarios adicionales sobre el alquiler.
        /// </summary>
        public string Comments { get; set; }
    }
}
