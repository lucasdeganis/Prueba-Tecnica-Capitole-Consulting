using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Interfaces
{
    /// <summary>
    /// Esta interfaz define las operaciones CRUD para la entidad Client.
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Recupera todos los clientes de la base de datos.
        /// </summary>
        /// <returns>Retorna una lista de clientes.</returns>
        Task<List<Client>> GetAllAsync();

        /// <summary>
        /// Obtiene un cliente por su identificador único.
        /// </summary>
        /// <param name="id">Identificador unico del cliente.</param>
        /// <returns>Retorna el cliente si lo encontro.</returns>
        Task<Client> GetByIdAsync(Guid id);

        /// <summary>
        /// rea un nuevo cliente en la base de datos.
        /// </summary>
        /// <param name="client">Recibe el cliente que se quiere dar de alta.</param>
        /// <returns>Devuelve un status en true si se guardo correctamente.</returns>
        Task CreateAsync(Client client);

        /// <summary>
        /// Actualiza un cliente existente en la base de datos.
        /// </summary>
        /// <param name="client">Recibe el cliente con las modificaciones a realizar.</param>
        /// <returns>Devuelve un status en true si se actualizo correctamente.</returns>
        Task UpdateAsync(Client client);

        /// <summary>
        /// Elimina un cliente de la base de datos por su identificador único.
        /// </summary>
        /// <param name="id">Recibe el identificador unico del cliente.</param>
        /// <returns>Devuelve un status en true si se elimina correctamente el cliente.</returns>
        Task DeleteAsync(Guid id);
    }
}
