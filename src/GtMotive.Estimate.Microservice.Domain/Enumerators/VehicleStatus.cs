namespace GtMotive.Estimate.Microservice.Domain.Enumerators
{
    /// <summary>
    /// Este enumerador representa los estados posibles de un vehículo en el sistema de gestión de flotas.
    /// </summary>
    public enum VehicleStatus
    {
        /// <summary>
        /// Estado del vehículo disponible para ser alquilado o utilizado.
        /// </summary>
        Available,

        /// <summary>
        /// Estado del vehículo actualmente alquilado por un cliente.
        /// </summary>
        Rented,

        /// <summary>
        /// Estado del vehículo que indica que está en mantenimiento o reparación.
        /// </summary>
        UnderMaintenance,

        /// <summary>
        /// Estado del vehículo que indica que no está disponible para su uso, ya sea por estar fuera de servicio o por otras razones.
        /// </summary>
        Unavailable,
    }
}
