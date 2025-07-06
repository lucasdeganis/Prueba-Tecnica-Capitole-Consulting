using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rent
{
    /// <summary>
    /// Esta clase representa la entrada para el caso de uso de devolución de vehículo.
    /// </summary>
    public class ReturnVehicleUseCaseInput : IUseCaseInput
    {
        /// <summary>
        /// Gets or sets el identificador único del vehículo que se está devolviendo.
        /// </summary>
        public Guid RentId { get; set; }

        /// <summary>
        /// Gets or sets la fecha de devolución del vehículo.
        /// </summary>
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// Gets or sets el identificador único del cliente que está devolviendo el vehículo.
        /// </summary>
        public int KilometersDriven { get; set; }

        /// <summary>
        /// Gets or sets el identificador único del cliente que está devolviendo el vehículo.
        /// </summary>
        public string Comments { get; set; }
    }
}
