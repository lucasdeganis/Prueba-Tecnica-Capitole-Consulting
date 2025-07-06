namespace GtMotive.Estimate.Microservice.Domain.Enumerators
{
    /// <summary>
    /// Este enumerador representa los tipos de combustible que puede utilizar un vehículo.
    /// </summary>
    public enum VehicleFuelType
    {
        /// <summary>
        /// Tipo de combustible: Gasolina.
        /// </summary>
        Petrol,

        /// <summary>
        /// Tipo de combustible: Diésel.
        /// </summary>
        Diesel,

        /// <summary>
        /// Tipo de combustible: Eléctrico.
        /// </summary>
        Electric,

        /// <summary>
        /// Tipo de combustible: Híbrido (combinación de motor de combustión interna y eléctrico).
        /// </summary>
        Hybrid,

        /// <summary>
        /// Tipo de combustible: Gas (GLP o GNC).
        /// </summary>
        LPG,
    }
}
