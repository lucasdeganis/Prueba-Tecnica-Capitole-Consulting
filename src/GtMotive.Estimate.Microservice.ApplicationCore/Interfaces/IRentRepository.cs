using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Interfaces
{
    /// <summary>
    /// Esta interfaz define las operaciones CRUD para la entidad Rent.
    /// </summary>
    public interface IRentRepository
    {
        /// <summary>
        /// Recupera todos los alquileres de la base de datos.
        /// </summary>
        /// <returns>Retorna una lista con todos los alquileres.</returns>
        Task<List<Rent>> GetAllAsync();

        /// <summary>
        /// Recupera un alquiler por su identificador único.
        /// </summary>
        /// <param name="id">Recibe el identificador unico del alquiler.</param>
        /// <returns>Retorna el alquiler si fue encontrado.</returns>
        Task<Rent> GetByIdAsync(Guid id);

        /// <summary>
        /// Crea un nuevo alquiler en la base de datos.
        /// </summary>
        /// <param name="rent">Recibe el alquiler que se debe crear.</param>
        /// <returns>Retorna un status en true si se guardo correctamente el alquiler.</returns>
        Task CreateAsync(Rent rent);

        /// <summary>
        /// Actualiza un alquiler existente en la base de datos.
        /// </summary>
        /// <param name="rent">Recibe el alquiler con los datos a actualizar.</param>
        /// <returns>Retorna un status en true si se actualiza correctamente el alquiler.</returns>
        Task UpdateAsync(Rent rent);

        /// <summary>
        /// Elimina un alquiler de la base de datos por su identificador único.
        /// </summary>
        /// <param name="id">Recibe el identificador unico del alquiler.</param>
        /// <returns>Retorna un status en true si se elimina correctamente el alquiler.</returns>
        Task DeleteAsync(Guid id);
    }
}
