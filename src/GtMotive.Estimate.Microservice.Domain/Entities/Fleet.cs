using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Esta clase representa una flota de vehículos en el sistema de gestión de flotas.
    /// </summary>
    public class Fleet
    {
        /// <summary>
        /// Gets or sets identidicador unico de la flota.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets el nombre de la flota.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets la descripción de la flota.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets el tipo de flota.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets el cliente asociado a la flota.
        /// </summary>
        public string ResponsiblePerson { get; set; }

        /// <summary>
        /// Gets or sets el cliente asociado a la flota.
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        /// Gets or sets el cliente asociado a la flota.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
