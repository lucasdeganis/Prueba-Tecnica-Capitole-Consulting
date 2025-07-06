using System.Collections.Generic;
using GtMotive.Estimate.Microservice.Domain.Dto;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Fleet
{
    /// <summary>
    /// Esta clase representa la salida del caso de uso para obtener flotas.
    /// </summary>
    public class GetFleetsUseCaseOutput : IUseCaseOutput
    {
        /// <summary>
        /// Gets or sets lista de flotas obtenidas.
        /// </summary>
        public List<FleetDto> Fleets { get; set; }

        /// <summary>
        /// Gets or sets mensaje de salida del caso de uso.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets un valor que indica si la operación fue exitosa.
        /// </summary>
        public bool Success { get; set; }
    }
}
