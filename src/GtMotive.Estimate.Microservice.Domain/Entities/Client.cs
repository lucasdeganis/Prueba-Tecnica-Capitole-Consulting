using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Esta clase representa un cliente en el sistema de gestión de flotas.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets or sets identificador único del cliente.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets el nombre del cliente.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets el apellido del cliente.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets el número de documento del cliente.
        /// </summary>
        public string DocumentNumber { get; set; } // e.g., National ID, Passport Number

        /// <summary>
        /// Gets or sets el tipo de documento del cliente.
        /// </summary>
        public string DocumentType { get; set; } // e.g., National ID, Passport

        /// <summary>
        /// Gets or sets el correo electrónico del cliente.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets el número de teléfono del cliente.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets la dirección del cliente.
        /// </summary>
        public string Address { get; set; } // e.g., Street, City, State, Zip Code

        /// <summary>
        /// Gets or sets el número de licencia del cliente.
        /// </summary>
        public string LicenseNumber { get; set; } // License number of the client

        /// <summary>
        /// Gets or sets la fecha de creación del cliente.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
