using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Fleet
{
    /// <summary>
    /// Esta clase representa la entrada del caso de uso para obtener vehículos por flota.
    /// </summary>
    public class GetVehiclesByFleetUseCaseInput : IUseCaseInput
    {
        /// <summary>
        /// Gets or sets el identificador único de la flota.
        /// </summary>
        public Guid FleetId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets un valor que indica si solo se deben recuperar vehículos activos.
        /// </summary>
        public bool OnlyActive { get; set; }
    }
}
