using System.Collections.Generic;
using GtMotive.Estimate.Microservice.Domain.Dto;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rent
{
    /// <summary>
    /// Esta clase representa la salida para el caso de uso de obtención de alquileres.
    /// </summary>
    public class GetRentsUseCaseOutput : IUseCaseOutput
    {
        /// <summary>
        /// Gets or sets identificador único del alquiler de vehículo.
        /// </summary>
        public List<RentDto> Rents { get; set; }

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
