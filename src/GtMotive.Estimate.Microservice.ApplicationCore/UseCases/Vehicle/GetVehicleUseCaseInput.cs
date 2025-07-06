using System;
using System.Text.Json.Serialization;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle
{
    /// <summary>
    /// Este caso de uso se encarga de obtener un vehículo por su identificador único.
    /// </summary>
    public class GetVehicleUseCaseInput : IUseCaseInput
    {
        /// <summary>
        /// Gets or sets el identificador único del vehículo.
        /// </summary>
        [JsonRequired]
        public Guid Id { get; set; }
    }
}
