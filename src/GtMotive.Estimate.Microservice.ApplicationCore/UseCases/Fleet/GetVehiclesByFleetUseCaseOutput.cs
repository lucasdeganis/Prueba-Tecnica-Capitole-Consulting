using System.Collections.Generic;
using GtMotive.Estimate.Microservice.Domain.Dto;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Fleet
{
    /// <summary>
    /// Este caso de uso permite obtener una lista de vehículos asociados a una flota específica.
    /// </summary>
    public class GetVehiclesByFleetUseCaseOutput : IUseCaseOutput
    {
        /// <summary>
        /// Gets or sets lista de vehículos asociados a la flota.
        /// </summary>
        public List<VehicleDto> Vehicles { get; set; }

        /// <summary>
        /// Gets or sets mensaje de salida del caso de uso.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets estado del caso de uso.
        /// </summary>
        public bool Success { get; set; }
    }
}
