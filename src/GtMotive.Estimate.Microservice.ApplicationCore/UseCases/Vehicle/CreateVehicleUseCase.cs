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
    /// Este caso de uso se encarga de crear un vehículo en la base de datos.
    /// </summary>
    public class CreateVehicleUseCase : IUseCaseWithPresenter<CreateVehicleUseCaseInput, CreateVehicleUseCaseOutput>
    {
        private readonly IAppLogger<CreateVehicleUseCase> _logger;
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IFleetRepository _fleetRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleUseCase"/> class.
        /// Este constructor inicializa el repositorio de vehículos y el repositorio de flotas.
        /// </summary>
        /// <param name="vehicleRepository">Debe recibir el repositorio de vehiculo para interactuar con los datos.</param>
        /// <param name="fleetRepository">Debe recibir el repositorio de flotas para interactuar con los datos.</param>
        /// <param name="logger">Debe recibir el logger para registrar eventos y errores.</param>
        /// <param name="mapper">Debe recibir mapper para gestionar las conversiones de entidades y Dto.</param>
        /// <exception cref="ArgumentNullException">Si no se inyectan algun repositorio esto debera dar error.</exception>
        public CreateVehicleUseCase(IVehicleRepository vehicleRepository, IFleetRepository fleetRepository, IAppLogger<CreateVehicleUseCase> logger, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _fleetRepository = fleetRepository ?? throw new ArgumentNullException(nameof(fleetRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Este metodo ejecuta el caso de uso para crear un vehículo en la base de datos.
        /// </summary>
        /// <param name="input">Recibe como parametro el vehiculo para dar de alta.</param>
        /// <param name="outputPort">Retorna un status en true si el vehiculo pudo darse de alta.</param>
        /// <param name="notFoundPort">Retorna una validacion si  no se encuentra algun recurso.</param>
        /// <param name="validationPort">Retorna una validacion controlada de negocio.</param>
        /// <returns>Retorta un status en true si el vehiculo pudo darse de alta.</returns>
        public async Task Execute(CreateVehicleUseCaseInput input, IOutputPortStandard<CreateVehicleUseCaseOutput> outputPort, IOutputPortNotFound notFoundPort, IOutputPortValidation validationPort)
        {
            ArgumentNullException.ThrowIfNull(outputPort);
            ArgumentNullException.ThrowIfNull(notFoundPort);
            ArgumentNullException.ThrowIfNull(validationPort);

            if (input == null)
            {
                validationPort.ValidationHandle("El input no puede ser nulo.");
                return;
            }

            if (input.Year + 5 < DateTime.Now.Year)
            {
                validationPort.ValidationHandle("El año del vehículo no puede ser más antiguo que 5 años desde el año actual.");
                return;
            }

            Domain.Entities.Fleet fleet = null;

            if (input.FleetId != Guid.Empty)
            {
                try
                {
                    fleet = await _fleetRepository.GetByIdAsync(input.FleetId);
                }
                catch (DatabaseException ex)
                {
                    _logger.LogError(ex, "Error al obtener la flota desde la base de datos.");
                    outputPort.StandardHandle(
                        new CreateVehicleUseCaseOutput
                        {
                            Success = false,
                            Message = $"Error al obtener la flota: {ex.Message}"
                        },
                        500);
                    return;
                }

                if (fleet == null)
                {
                    notFoundPort.NotFoundHandle("Flota no encontrada.");
                    return;
                }
            }

            var vehicle = _mapper.Map<Domain.Entities.Vehicle>(input);
            vehicle.Fleet = fleet;
            vehicle.Status = Domain.Enumerators.VehicleStatus.Available;
            vehicle.CreatedAt = DateTime.UtcNow;

            try
            {
                await _vehicleRepository.CreateAsync(vehicle);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Error al crear el vehículo en la base de datos.");
                outputPort.StandardHandle(
                    new CreateVehicleUseCaseOutput
                    {
                        Success = false,
                        Message = $"Error al crear el vehículo: {ex.Message}"
                    },
                    500);
                return;
            }

            var output = new CreateVehicleUseCaseOutput
            {
                Vehicle = _mapper.Map<VehicleDto>(vehicle),
                Success = true,
                Message = "Vehículo creado satifactoriamente."
            };

            outputPort.StandardHandle(output);
        }
    }
}
