namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Fleet
{
    /// <summary>
    /// Esta clase define la salida del caso de uso de añadir un vehículo a una flota.
    /// </summary>
    public class AddVehicleToFleetUseCaseOutput : IUseCaseOutput
    {
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
