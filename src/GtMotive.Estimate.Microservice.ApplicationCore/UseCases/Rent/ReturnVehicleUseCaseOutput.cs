namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rent
{
    /// <summary>
    /// Esta clase define la salida del caso de uso de devolver un vehículo alquilado.
    /// </summary>
    public class ReturnVehicleUseCaseOutput : IUseCaseOutput
    {
        /// <summary>
        /// Gets or sets mensaje de salida del caso de uso.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets Indica si la operación fue exitosa.
        /// </summary>
        public bool Success { get; set; }
    }
}
