using GtMotive.Estimate.Microservice.Domain.Dto;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle
{
    /// <summary>
    /// Este caso de uso se encarga de obtener un vehículo por su identificador único.
    /// </summary>
    public class GetVehicleUseCaseOutput : IUseCaseOutput
    {
        /// <summary>
        /// Gets or sets el vehículo obtenido.
        /// </summary>
        public VehicleDto Vehicle { get; set; }

        /// <summary>
        /// Gets or sets un mensaje que describe el resultado de la operación.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets un estado que indica si la operación fue exitosa.
        /// </summary>
        public bool Success { get; set; }
    }
}
