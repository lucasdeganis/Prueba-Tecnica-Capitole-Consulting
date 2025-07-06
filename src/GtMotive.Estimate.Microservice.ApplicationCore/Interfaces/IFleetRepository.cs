using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Interfaces
{
    /// <summary>
    /// Esta interfaz define las operaciones CRUD para la entidad Fleet.
    /// </summary>
    public interface IFleetRepository
    {
        /// <summary>
        /// Recupera todos las flotas de la base de datos.
        /// </summary>
        /// <returns>Retorna una lista de flotas.</returns>
        Task<List<Fleet>> GetAllAsync();

        /// <summary>
        /// Recupera una flota por su identificador único.
        /// </summary>
        /// <param name="id">Recibe el identificador unico de la flota.</param>
        /// <returns>Retorna la flota si fue encontrada.</returns>
        Task<Fleet> GetByIdAsync(Guid id);

        /// <summary>
        /// Crea una nueva flota en la base de datos.
        /// </summary>
        /// <param name="fleet">Recibe la flota que se debe dar de crear.</param>
        /// <returns>Retorna un status en true si se creo la flota correctamente.</returns>
        Task CreateAsync(Fleet fleet);

        /// <summary>
        /// Actualiza una flota existente en la base de datos.
        /// </summary>
        /// <param name="fleet">Recibe la flota con los cambios que hay que actualizar.</param>
        /// <returns>Retorna un status en true si se actualiza correcamente la flota.</returns>
        Task UpdateAsync(Fleet fleet);

        /// <summary>
        /// Elimina una flota de la base de datos por su identificador único.
        /// </summary>
        /// <param name="id">Recibe el identificaro de la flota a borrar.</param>
        /// <returns>Retorna un status en true si se elimina correctamente la flota.</returns>
        Task DeleteAsync(Guid id);
    }
}
