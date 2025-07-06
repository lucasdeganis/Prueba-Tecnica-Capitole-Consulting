using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rent
{
    /// <summary>
    /// Esta clase representa la salida para el caso de uso de alquiler de vehículo.
    /// </summary>
    public class RentVehicleUseCaseOutput : IUseCaseOutput
    {
        /// <summary>
        /// Gets or sets identificador único del alquiler de vehículo.
        /// </summary>
        public Guid RentId { get; set; }

        /// <summary>
        /// Gets or sets mensaje de salida del caso de uso.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets estado del alquiler de vehículo.
        /// </summary>
        public bool Success { get; set; }
    }
}
