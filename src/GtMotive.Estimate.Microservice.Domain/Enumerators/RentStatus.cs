namespace GtMotive.Estimate.Microservice.Domain.Enumerators
{
    /// <summary>
    /// Este enumerador representa los posibles estados de un alquiler de vehículo.
    /// </summary>
    public enum RentStatus
    {
        /// <summary>
        /// Estado del alquiler activo.
        /// </summary>
        Active,

        /// <summary>
        /// Estado del alquiler completado.
        /// </summary>
        Completed,

        /// <summary>
        /// Estado del alquiler cancelado.
        /// </summary>
        Cancelled,
    }
}
