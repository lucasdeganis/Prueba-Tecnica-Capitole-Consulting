using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Infrastructure;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories
{
    public class RepositoryBase
    {
        public async Task<T> ExecuteWithExceptionHandling<T>(Func<Task<T>> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func), "El parámetro func no puede ser nulo.");
            }

            try
            {
                return await func();
            }
            catch (MongoAuthenticationException ex)
            {
                // Problemas de autenticación
                throw new DatabaseException("Error de autenticación en la base de datos.", ex);
            }
            catch (MongoConnectionException ex)
            {
                // Problemas de conexión
                throw new DatabaseException("Error de conexión a la base de datos.", ex);
            }
            catch (TimeoutException ex)
            {
                // Timeout
                throw new DatabaseException("Tiempo de espera agotado en la operación de base de datos.", ex);
            }
            catch (MongoWriteException ex)
            {
                // Por ejemplo, clave duplicada
                if (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
                {
                    // Manejar duplicado
                    throw new DatabaseException("Error de clave duplicada en la base de datos.", ex);
                }
                else
                {
                    // Otro error de escritura
                    throw new DatabaseException("Error de escritura en la base de datos.", ex);
                }
            }
            catch (MongoException ex)
            {
                // Cualquier otro error de MongoDB
                throw new DatabaseException("Error en la operación de base de datos.", ex);
            }
        }

        public async Task ExecuteWithExceptionHandling(Func<Task> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func), "El parámetro func no puede ser nulo.");
            }

            try
            {
                await func();
            }
            catch (MongoAuthenticationException ex)
            {
                // Problemas de autenticación
                throw new DatabaseException("Error de autenticación en la base de datos.", ex);
            }
            catch (MongoConnectionException ex)
            {
                // Problemas de conexión
                throw new DatabaseException("Error de conexión a la base de datos.", ex);
            }
            catch (TimeoutException ex)
            {
                // Timeout
                throw new DatabaseException("Tiempo de espera agotado en la operación de base de datos.", ex);
            }
            catch (MongoWriteException ex)
            {
                // Por ejemplo, clave duplicada
                if (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
                {
                    // Manejar duplicado
                    throw new DatabaseException("Error de clave duplicada en la base de datos.", ex);
                }
                else
                {
                    // Otro error de escritura
                    throw new DatabaseException("Error de escritura en la base de datos.", ex);
                }
            }
            catch (MongoException ex)
            {
                // Cualquier otro error de MongoDB
                throw new DatabaseException("Error en la operación de base de datos.", ex);
            }
        }
    }
}
