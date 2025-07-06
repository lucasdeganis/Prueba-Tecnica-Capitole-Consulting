using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Fleet
{
    /// <summary>
    /// Este caso de uso permite agregar un vehículo a una flota específica.
    /// </summary>
    public class AddVehicleToFleetUseCaseInput : IUseCaseInput
    {
        /// <summary>
        /// Gets or sets el identificador único de la flota a la que se agregará el vehículo.
        /// </summary>
        public Guid FleetId { get; set; }

        /// <summary>
        /// Gets or sets el identificador único del vehículo que se agregará a la flota.
        /// </summary>
        public Guid VehicleId { get; set; }
    }
}
