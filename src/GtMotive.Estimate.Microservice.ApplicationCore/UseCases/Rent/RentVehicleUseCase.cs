using System;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Infrastructure;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rent
{
    /// <summary>
    /// Este caso de uso permite alquilar un vehículo a un cliente.
    /// </summary>
    public class RentVehicleUseCase : IUseCaseWithPresenter<RentVehicleUseCaseInput, RentVehicleUseCaseOutput>
    {
        private readonly IAppLogger<RentVehicleUseCase> _logger;
        private readonly IMapper _mapper;
        private readonly IRentRepository _rentRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IClientRepository _clientRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleUseCase"/> class.
        /// Este constructor inicializa las dependencias necesarias para el caso de uso de alquiler de vehículo.
        /// </summary>
        /// <param name="rentRepository">Debe recibir el repositorio de alquiler para la gestion de datos.</param>
        /// <param name="vehicleRepository">Debe recibir el repositorio de vehiculo para la gestion de datos.</param>
        /// <param name="clientRepository">Debe recibir el repositorio de cliente para la gestio de datos.</param>
        /// <param name="mapper">Debe recibir mapper para gestionar las conversiones de entidades y Dto.</param>
        /// <param name="logger">Debe recibir el logger para registrar eventos y errores.</param>
        /// <exception cref="ArgumentNullException">Lanza una excepción si alguno de los repositorios es nulo.</exception>
        public RentVehicleUseCase(IRentRepository rentRepository, IVehicleRepository vehicleRepository, IClientRepository clientRepository, IAppLogger<RentVehicleUseCase> logger, IMapper mapper)
        {
            _rentRepository = rentRepository ?? throw new ArgumentNullException(nameof(rentRepository));
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Este método ejecuta el caso de uso de alquiler de vehículo.
        /// </summary>
        /// <param name="input">Recibe la informacion de alquiler de un vehiculo.</param>
        /// <param name="outputPort">Retorna un status en true si el alquiler es exitoso.</param>
        /// <param name="notFoundPort">Retorna un mensaje si no se encontro algun recurso.</param>
        /// <param name="validationPort">Puerto de salida para manejar validaciones de negocio.</param>
        /// <exception cref="ArgumentNullException">Lanza una excepción si alguno de los puertos es nulo.</exception>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task Execute(RentVehicleUseCaseInput input, IOutputPortStandard<RentVehicleUseCaseOutput> outputPort, IOutputPortNotFound notFoundPort, IOutputPortValidation validationPort)
        {
            ArgumentNullException.ThrowIfNull(outputPort);
            ArgumentNullException.ThrowIfNull(notFoundPort);
            ArgumentNullException.ThrowIfNull(validationPort);

            if (input == null)
            {
                validationPort.ValidationHandle("El input no puede ser nulo.");
                return;
            }

            if (input.RentStartDate >= input.RentEndDate)
            {
                validationPort.ValidationHandle("La fecha de inicio debe ser anterior a la fecha de fin.");
                return;
            }

            if (input.RentStartDate < DateTime.UtcNow)
            {
                validationPort.ValidationHandle("La fecha de inicio no puede ser anterior a la fecha actual.");
                return;
            }

            if (input.RentEndDate < DateTime.UtcNow)
            {
                validationPort.ValidationHandle("La fecha de fin no puede ser anterior a la fecha actual.");
                return;
            }

            var hasPreviwRent = (await _rentRepository.GetAllAsync()).Exists(x => x.ClientId == input.ClientId && x.Status == Domain.Enumerators.RentStatus.Active);
            if (hasPreviwRent)
            {
                validationPort.ValidationHandle("El cliente ya tiene una reserva activa.");
                return;
            }

            Domain.Entities.Vehicle vehicle;
            try
            {
                vehicle = await _vehicleRepository.GetByIdAsync(input.VehicleId);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Error al obtener el vehiculo con ID {VehicleId}", input.VehicleId);
                outputPort.StandardHandle(
                    new RentVehicleUseCaseOutput
                    {
                        Success = false,
                        Message = "Error al obtener el vehiculo."
                    },
                    500);
                return;
            }

            if (vehicle == null)
            {
                notFoundPort.NotFoundHandle("No se encontro el vehiculo.");
                return;
            }

            if (vehicle.Fleet == null || vehicle.Fleet.Id == Guid.Empty)
            {
                notFoundPort.NotFoundHandle("El vehiculo no pertenece a una flota.");
                return;
            }

            if (vehicle.Status != Domain.Enumerators.VehicleStatus.Available)
            {
                validationPort.ValidationHandle("El vehiculo no se encuentra disponible.");
                return;
            }

            if (vehicle.Year < DateTime.UtcNow.Year - 5)
            {
                vehicle.Status = Domain.Enumerators.VehicleStatus.Unavailable;
                await _vehicleRepository.UpdateAsync(vehicle);

                validationPort.ValidationHandle("El vehiculo no puede ser alquilado por antiguedad.");
                return;
            }

            Domain.Entities.Client client;
            try
            {
                client = await _clientRepository.GetByIdAsync(input.ClientId);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Error al obtener el cliente con ID {ClientId}", input.ClientId);
                outputPort.StandardHandle(
                    new RentVehicleUseCaseOutput
                    {
                        Success = false,
                        Message = $"Error al obtener el cliente: {ex.Message}"
                    },
                    500);
                return;
            }

            if (client == null)
            {
                notFoundPort.NotFoundHandle("No se encontro el cliente.");
                return;
            }

            var rent = _mapper.Map<Domain.Entities.Rent>(input);
            rent.Vehicle = vehicle;
            rent.Client = client;
            rent.Status = Domain.Enumerators.RentStatus.Active;
            rent.ActualKilometers = vehicle.Kilometer;
            rent.KilometersDriven = 0;
            rent.CreatedAt = DateTime.UtcNow;

            try
            {
                await _rentRepository.CreateAsync(rent);
                vehicle.Status = Domain.Enumerators.VehicleStatus.Rented;
                await _vehicleRepository.UpdateAsync(vehicle);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Error al crear el alquiler o actualizar el vehículo.");
                outputPort.StandardHandle(
                    new RentVehicleUseCaseOutput
                    {
                        Success = false,
                        Message = $"Error al devolver el vehículo: {ex.Message}"
                    },
                    500);
                return;
            }

            var output = new RentVehicleUseCaseOutput
            {
                RentId = rent.Id,
                Success = true,
                Message = "Se realizo la reserva satifactoriamente."
            };

            outputPort.StandardHandle(output);
        }
    }
}
