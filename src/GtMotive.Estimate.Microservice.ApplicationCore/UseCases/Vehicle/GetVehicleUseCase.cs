using System;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Dto;
using GtMotive.Estimate.Microservice.Domain.Infrastructure;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle
{
    /// <summary>
    /// Caso de uso para obtener un vehículo por su identificador único.
    /// </summary>
    public class GetVehicleUseCase : IUseCaseWithPresenter<GetVehicleUseCaseInput, GetVehicleUseCaseOutput>
    {
        private readonly IAppLogger<GetVehicleUseCase> _logger;
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehicleUseCase"/> class.
        /// Constructor que inicializa el repositorio de vehículos.
        /// </summary>
        /// <param name="vehicleRepository">Debe recibir el repositorio de vehiculo para interactuar con los datos.</param>
        /// <param name="logger">Debe recibir el logger para registrar eventos y errores.</param>
        /// <param name="mapper">Debe recibir mapper para gestionar las conversiones de entidades y Dto.</param>
        public GetVehicleUseCase(IVehicleRepository vehicleRepository, IAppLogger<GetVehicleUseCase> logger, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Ejecuta el caso de uso para obtener un vehículo por su identificador único.
        /// </summary>
        /// <param name="input">Identificador de vehiculo.</param>
        /// <param name="outputPort">Vehiculo encontrado.</param>
        /// <param name="notFoundPort">Mensaje si no se encontro algun recurso.</param>
        /// <param name="validationPort">Mensaje si hay algun error controlado de datos.</param>
        /// <returns>Retorna el vehiculo encontrado.</returns>
        public async Task Execute(GetVehicleUseCaseInput input, IOutputPortStandard<GetVehicleUseCaseOutput> outputPort, IOutputPortNotFound notFoundPort, IOutputPortValidation validationPort)
        {
            ArgumentNullException.ThrowIfNull(outputPort);
            ArgumentNullException.ThrowIfNull(notFoundPort);
            ArgumentNullException.ThrowIfNull(validationPort);

            if (input == null)
            {
                validationPort.ValidationHandle("El input no puede ser nulo.");
                return;
            }

            Domain.Entities.Vehicle vehicle;
            try
            {
                vehicle = await _vehicleRepository.GetByIdAsync(input.Id);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Error al obtener el vehículo con ID {VehicleId}", input.Id);
                outputPort.StandardHandle(
                    new GetVehicleUseCaseOutput
                    {
                        Success = false,
                        Message = $"Error al obtener el vehículo: {ex.Message}"
                    },
                    500);
                return;
            }

            if (vehicle == null)
            {
                notFoundPort.NotFoundHandle("Vehículo no encontrado.");
                return;
            }

            var output = new GetVehicleUseCaseOutput
            {
                Vehicle = _mapper.Map<VehicleDto>(vehicle),
                Success = true,
                Message = "Vehículo encontrado."
            };

            outputPort.StandardHandle(output);
        }
    }
}
