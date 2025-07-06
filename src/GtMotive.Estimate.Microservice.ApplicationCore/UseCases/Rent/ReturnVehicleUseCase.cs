using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Infrastructure;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rent
{
    /// <summary>
    /// Esta clase representa el caso de uso para devolver un vehículo alquilado.
    /// </summary>
    public class ReturnVehicleUseCase : IUseCaseWithPresenter<ReturnVehicleUseCaseInput, ReturnVehicleUseCaseOutput>
    {
        private readonly IAppLogger<ReturnVehicleUseCase> _logger;
        private readonly IRentRepository _rentRepository;
        private readonly IVehicleRepository _vehicleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleUseCase"/> class.
        /// Este constructor inicializa las dependencias necesarias para el caso de uso de devolución de vehículo.
        /// </summary>
        /// <param name="rentRepository">Debe recibir el repositorio de alquiler para que pueda gestionar con los datos.</param>
        /// <param name="vehicleRepository">Debe recibir el repositorio de vehiculo para que pueda gestionar con los datos.</param>
        /// <param name="logger">Debe recibir el logger para registrar eventos y errores.</param>
        public ReturnVehicleUseCase(IRentRepository rentRepository, IVehicleRepository vehicleRepository, IAppLogger<ReturnVehicleUseCase> logger)
        {
            _rentRepository = rentRepository ?? throw new ArgumentNullException(nameof(rentRepository));
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Este método ejecuta el caso de uso de devolución de vehículo.
        /// </summary>
        /// <param name="input">Recibe la identificación de alquiler para devolver.</param>
        /// <param name="outputPort">Puerto de salida estándar para manejar la respuesta del caso de uso.</param>
        /// <param name="notFoundPort">Puerto de salida para manejar recursos no encontrados.</param>
        /// <param name="validationPort">Puerto de salida para manejar validaciones de negocio.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task Execute(ReturnVehicleUseCaseInput input, IOutputPortStandard<ReturnVehicleUseCaseOutput> outputPort, IOutputPortNotFound notFoundPort, IOutputPortValidation validationPort)
        {
            ArgumentNullException.ThrowIfNull(outputPort);
            ArgumentNullException.ThrowIfNull(notFoundPort);
            ArgumentNullException.ThrowIfNull(validationPort);

            if (input == null)
            {
                validationPort.ValidationHandle("El input no puede ser nulo.");
                return;
            }

            if (input.RentId == Guid.Empty)
            {
                validationPort.ValidationHandle("El ID de la reserva no puede ser nulo.");
                return;
            }

            var rent = await _rentRepository.GetByIdAsync(input.RentId);
            if (rent == null)
            {
                notFoundPort.NotFoundHandle("Reserva no encontrada.");
                return;
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(rent.VehicleId);
            if (vehicle == null)
            {
                notFoundPort.NotFoundHandle("Vehículo no encontrado.");
                return;
            }

            // Actualizar los datos de la reserva
            if (rent.EndDate < DateTime.UtcNow.AddHours(1))
            {
                rent.TotalCost += vehicle.PricePerHour * (int)(DateTime.UtcNow - rent.EndDate).TotalHours;
            }

            rent.EndDate = input.ReturnDate;
            rent.Status = Domain.Enumerators.RentStatus.Completed;
            rent.KilometersDriven = input.KilometersDriven;
            rent.Comments = input.Comments;

            // Actualizar el vehículo
            vehicle.Kilometer += input.KilometersDriven;
            vehicle.Status = Domain.Enumerators.VehicleStatus.Available;

            try
            {
                await _rentRepository.UpdateAsync(rent);
                await _vehicleRepository.UpdateAsync(vehicle);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Error al devolver el vehículo con ID {VehicleId}", vehicle.Id);
                outputPort.StandardHandle(
                    new ReturnVehicleUseCaseOutput
                    {
                        Success = false,
                        Message = $"Error al devolver el vehículo: {ex.Message}"
                    },
                    500);
                return;
            }

            outputPort.StandardHandle(new ReturnVehicleUseCaseOutput
            {
                Success = true,
                Message = "Vehículo devuelto correctamente."
            });
        }
    }
}
