using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Dto;
using GtMotive.Estimate.Microservice.Domain.Enumerators;
using GtMotive.Estimate.Microservice.Domain.Infrastructure;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Fleet
{
    /// <summary>
    /// Este caso de uso permite obtener los vehículos asociados a una flota específica.
    /// </summary>
    public class GetVehiclesByFleetUseCase : IUseCaseWithPresenter<GetVehiclesByFleetUseCaseInput, GetVehiclesByFleetUseCaseOutput>
    {
        private readonly IAppLogger<GetVehiclesByFleetUseCase> _logger;
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehiclesByFleetUseCase"/> class.
        /// Este constructor inicializa las dependencias necesarias para el caso de uso de obtención de vehículos por flota.
        /// </summary>
        /// <param name="vehicleRepository">Debe recibir el repositorio de vehiculos para la gestion de datos.</param>
        /// <param name="logger">Debe recibir el logger para registrar eventos y errores.</param>
        /// <param name="mapper">Debe recibir mapper para gestionar las conversiones de entidades y Dto.</param>
        /// <exception cref="ArgumentNullException">Debuelve una exepcion si las dependencias son null.</exception>
        public GetVehiclesByFleetUseCase(IVehicleRepository vehicleRepository, IAppLogger<GetVehiclesByFleetUseCase> logger, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Este método ejecuta el caso de uso de obtención de vehículos por flota.
        /// </summary>
        /// <param name="input">Recibe como dato de entrada el identificador unico de la flota.</param>
        /// <param name="outputPort">Retorna un status en true si se devuelve una lista de vehiculos.</param>
        /// <param name="notFoundPort">Retorna un mensaje si no se encontro algun recurso.</param>
        /// <param name="validationPort">Puerto de salida para manejar validaciones de negocio.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task Execute(GetVehiclesByFleetUseCaseInput input, IOutputPortStandard<GetVehiclesByFleetUseCaseOutput> outputPort, IOutputPortNotFound notFoundPort, IOutputPortValidation validationPort)
        {
            ArgumentNullException.ThrowIfNull(outputPort);
            ArgumentNullException.ThrowIfNull(notFoundPort);
            ArgumentNullException.ThrowIfNull(validationPort);

            if (input == null)
            {
                validationPort.ValidationHandle("El input no puede ser nulo.");
                return;
            }

            if (input.FleetId == Guid.Empty)
            {
                validationPort.ValidationHandle("El id de la flota no puede ser nulo.");
                return;
            }

            Expression<Func<Domain.Entities.Vehicle, bool>> filter = v => v.Fleet.Id == input.FleetId;

            if (input.OnlyActive)
            {
                // Combina la condición existente con la nueva usando AndAlso
                filter = CombineAnd(filter, v => v.Status == VehicleStatus.Available);
            }

            List<Domain.Entities.Vehicle> vehicles;
            try
            {
                vehicles = await _vehicleRepository.GetQueryAsync(filter);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Error al obtener los vehiculos desde la base de datos.");
                outputPort.StandardHandle(
                    new GetVehiclesByFleetUseCaseOutput
                    {
                        Success = false,
                        Message = $"Error al obtener los vehiculos: {ex.Message}"
                    },
                    500);
                return;
            }

            if (vehicles == null)
            {
                notFoundPort.NotFoundHandle("No se encontro vehiculos.");
                return;
            }

            var output = new GetVehiclesByFleetUseCaseOutput
            {
                Vehicles = _mapper.Map<List<VehicleDto>>(vehicles),
                Success = true,
                Message = "La flota coniene los siguientes vehiculos."
            };

            outputPort.StandardHandle(output);
        }

        private static Expression<Func<T, bool>> CombineAnd<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));
            var body = Expression.AndAlso(Expression.Invoke(expr1, parameter), Expression.Invoke(expr2, parameter));

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
