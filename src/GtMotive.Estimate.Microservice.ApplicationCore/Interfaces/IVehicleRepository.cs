using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Interfaces
{
    /// <summary>
    /// Esta interfaz define las operaciones CRUD para la entidad Vehicle.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Recupera una lista de vehículos que cumplen con el predicado especificado.
        /// </summary>
        /// <param name="predicate">Recibe un filtro a aplicar para obtener los vehiculos.</param>
        /// <returns>Retorna una lista de vehiculos.</returns>
        Task<List<Vehicle>> GetQueryAsync(Expression<Func<Vehicle, bool>> predicate);

        /// <summary>
        /// Recupera todos los vehículos de la base de datos.
        /// </summary>
        /// <returns>Retorna una lista de vehiculos.</returns>
        Task<List<Vehicle>> GetAllAsync();

        /// <summary>
        /// Recupera un vehículo por su identificador único.
        /// </summary>
        /// <param name="id">Recibe el identificador unico del vehiculo.</param>
        /// <returns>Retorna el vehiculo si lo encuentra.</returns>
        Task<Vehicle> GetByIdAsync(Guid id);

        /// <summary>
        /// Crea un nuevo vehículo en la base de datos.
        /// </summary>
        /// <param name="vehicle">Recibe el vehiculo que se quiere guardar.</param>
        /// <returns>Retorna un status en true si se guarda correctamente el vehiculo.</returns>
        Task CreateAsync(Vehicle vehicle);

        /// <summary>
        /// Actualiza un vehículo existente en la base de datos.
        /// </summary>
        /// <param name="vehicle">Recibe el vehiculo con los datos a actualizar.</param>
        /// <returns>Retorna un status en true si se actualiza correctamente el vehiculo.</returns>
        Task UpdateAsync(Vehicle vehicle);

        /// <summary>
        /// Elimina un vehículo de la base de datos por su identificador único.
        /// </summary>
        /// <param name="id">Recibe el identificador unico del vehiculo a eliminar.</param>
        /// <returns>Retorna un status en true si se elimina correctamente el vehiculo.</returns>
        Task DeleteAsync(Guid id);
    }
}
