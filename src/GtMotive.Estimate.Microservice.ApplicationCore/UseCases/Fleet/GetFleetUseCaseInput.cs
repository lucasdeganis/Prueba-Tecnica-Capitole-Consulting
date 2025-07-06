using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Fleet
{
    /// <summary>
    /// Esta clase representa la entrada del caso de uso para obtener una flota por su identificador único.
    /// </summary>
    public class GetFleetUseCaseInput : IUseCaseInput
    {
        /// <summary>
        /// Gets or sets identificador único de la flota que se desea obtener.
        /// </summary>
        public Guid FleetId { get; set; }
    }
}
