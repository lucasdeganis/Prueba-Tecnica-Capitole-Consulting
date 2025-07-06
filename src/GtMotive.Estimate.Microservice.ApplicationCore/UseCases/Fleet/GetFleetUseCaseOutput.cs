using GtMotive.Estimate.Microservice.Domain.Dto;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Fleet
{
    /// <summary>
    /// Esta clase representa la salida del caso de uso para obtener una flota específica.
    /// </summary>
    public class GetFleetUseCaseOutput : IUseCaseOutput
    {
        /// <summary>
        /// Gets or sets la flota obtenida.
        /// </summary>
        public FleetDto Fleet { get; set; }

        /// <summary>
        /// Gets or sets mensaje de salida del caso de uso.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets a value que indica si la operación fue exitosa.
        /// </summary>
        public bool Success { get; set; }
    }
}
