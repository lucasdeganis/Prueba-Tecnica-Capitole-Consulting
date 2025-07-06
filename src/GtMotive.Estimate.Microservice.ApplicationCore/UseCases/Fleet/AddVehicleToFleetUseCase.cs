using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Infrastructure;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Fleet
{
    /// <summary>
    /// Este caso de uso permite agregar un vehículo a una flota específica.
    /// </summary>
    public class AddVehicleToFleetUseCase : IUseCaseWithPresenter<AddVehicleToFleetUseCaseInput, AddVehicleToFleetUseCaseOutput>
    {
        private readonly IAppLogger<AddVehicleToFleetUseCase> _logger;

        private readonly IFleetRepository _fleetRepository;
        private readonly IVehicleRepository _vehicleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddVehicleToFleetUseCase"/> class.
        /// Este constructor inicializa las dependencias necesarias para el caso de uso de agregar un vehículo a una flota.
        /// </summary>
        /// <param name="fleetRepository">Debe recibir el repositorio de flotas para la gestion de datos.</param>
        /// <param name="vehicleRepository">Debe recibir el repositorio de vehiculos para la gestion de datos.</param>
        /// <param name="logger">Debe recibir el logger para registrar eventos y errores.</param>
        /// <exception cref="ArgumentNullException">Debuelve una exepcion si las dependencias son null.</exception>
        public AddVehicleToFleetUseCase(IFleetRepository fleetRepository, IVehicleRepository vehicleRepository, IAppLogger<AddVehicleToFleetUseCase> logger)
        {
            _fleetRepository = fleetRepository ?? throw new ArgumentNullException(nameof(fleetRepository));
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Este método ejecuta el caso de uso de agregar un vehículo a una flota.
        /// </summary>
        /// <param name="input">Recibe como dato de entrada el el vehiculo que se quiere agregar a la flota.</param>
        /// <param name="outputPort">Retorna un status en true si se adiciona correctamente el vehiculos.</param>
        /// <param name="notFoundPort">Retorna un mensaje si no se encontro algun recurso.</param>
        /// <param name="validationPort">Puerto de salida para manejar validaciones de negocio.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task Execute(AddVehicleToFleetUseCaseInput input, IOutputPortStandard<AddVehicleToFleetUseCaseOutput> outputPort, IOutputPortNotFound notFoundPort, IOutputPortValidation validationPort)
        {
            ArgumentNullException.ThrowIfNull(outputPort);
            ArgumentNullException.ThrowIfNull(notFoundPort);
            ArgumentNullException.ThrowIfNull(validationPort);

            if (input == null)
            {
                validationPort.ValidationHandle("El input no puede ser nulo.");
                return;
            }

            if (input.VehicleId == Guid.Empty)
            {
                validationPort.ValidationHandle("El VehicleId debe ser válido.");
                return;
            }

            if (input.FleetId == Guid.Empty)
            {
                validationPort.ValidationHandle("El FleetId debe ser válidos.");
                return;
            }

            Domain.Entities.Fleet fleet;
            try
            {
                fleet = await _fleetRepository.GetByIdAsync(input.FleetId);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Error al obtener la flota con ID {FleetId}", input.FleetId);
                outputPort.StandardHandle(
                    new AddVehicleToFleetUseCaseOutput
                    {
                        Success = false,
                        Message = "Error al obtener la flota: " + ex.Message
                    },
                    500);
                return;
            }

            if (fleet == null)
            {
                notFoundPort.NotFoundHandle("No se encontro la flota.");
                return;
            }

            Domain.Entities.Vehicle vehicle;
            try
            {
                vehicle = await _vehicleRepository.GetByIdAsync(input.VehicleId);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Error al obtener el vehículo con ID {VehicleId}", input.VehicleId);
                outputPort.StandardHandle(
                    new AddVehicleToFleetUseCaseOutput
                    {
                        Success = false,
                        Message = "Error al obtener el vehículo: " + ex.Message
                    },
                    500);
                return;
            }

            if (vehicle.Fleet != null && vehicle.Fleet.Id != Guid.Empty)
            {
                notFoundPort.NotFoundHandle("El vehiculo ya se encuentra asociado a una flota.");
                return;
            }

            if (vehicle.Year < DateTime.UtcNow.Year - 5)
            {
                notFoundPort.NotFoundHandle("El vehiculo no puede ser alquilado por antiguedad.");
                return;
            }

            vehicle.Fleet = fleet;
            try
            {
                await _vehicleRepository.UpdateAsync(vehicle);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Error al agregar el vehículo con ID {VehicleId} a la flota con ID {FleetId}", input.VehicleId, input.FleetId);
                outputPort.StandardHandle(
                    new AddVehicleToFleetUseCaseOutput
                    {
                        Success = false,
                        Message = "Error al agregar el vehículo a la flota: " + ex.Message
                    },
                    500);
            }

            outputPort.StandardHandle(new AddVehicleToFleetUseCaseOutput
            {
                Success = true,
                Message = "Vehículo agregado a la flota correctamente."
            });
        }
    }
}
