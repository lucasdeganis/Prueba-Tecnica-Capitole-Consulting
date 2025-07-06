using System;

namespace GtMotive.Estimate.Microservice.Domain.Dto
{
    /// <summary>
    /// Esta clase representa un DTO (Data Transfer Object) para la entidad Fleet.
    /// </summary>
    public class FleetDto
    {
        /// <summary>
        /// Gets or sets identificador único de la flota.
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
        /// Gets or sets el número de vehículos en la flota.
        /// </summary>
        public string ResponsiblePerson { get; set; }

        /// <summary>
        /// Gets or sets fecha de creacion de la flota.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
