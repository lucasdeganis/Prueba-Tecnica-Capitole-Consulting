using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Dto;
using GtMotive.Estimate.Microservice.Domain.Infrastructure;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle
{
    /// <summary>
    /// Este caso de uso se encarga de obtener una lista de vehículos.
    /// </summary>
    public class GetVehiclesUseCase : IUseCaseWithPresenter<GetVehiclesUseCaseInput, GetVehiclesUseCaseOutput>
    {
        private readonly IAppLogger<GetVehicleUseCase> _logger;
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehiclesUseCase"/> class.
        /// Este constructor inicializa el repositorio de vehículos y el logger.
        /// </summary>
        /// <param name="vehicleRepository">Debe recibir el repositorio de vehiculo para interactuar con los datos.</param>
        /// <param name="logger">Debe recibir el logger para registrar eventos y errores.</param>
        /// <param name="mapper">Debe recibir mapper para gestionar las conversiones de entidades y Dto.</param>
        /// <exception cref="ArgumentNullException">Lanza una excepción si alguno de los repositorios es nulo.</exception>
        public GetVehiclesUseCase(IVehicleRepository vehicleRepository, IAppLogger<GetVehicleUseCase> logger, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Es método que ejecuta el caso de uso para obtener una lista de vehículos.
        /// </summary>
        /// <param name="input">Identificador de vehiculo.</param>
        /// <param name="outputPort">Vehiculo encontrado.</param>
        /// <param name="notFoundPort">Mensaje si no se encontro algun recurso.</param>
        /// <param name="validationPort">Mensaje si hay algun error controlado de datos.</param>
        /// <returns>Retorna el vehiculo encontrado.</returns>
        public async Task Execute(GetVehiclesUseCaseInput input, IOutputPortStandard<GetVehiclesUseCaseOutput> outputPort, IOutputPortNotFound notFoundPort, IOutputPortValidation validationPort)
        {
            ArgumentNullException.ThrowIfNull(outputPort);
            ArgumentNullException.ThrowIfNull(notFoundPort);
            ArgumentNullException.ThrowIfNull(validationPort);

            List<Domain.Entities.Vehicle> vehicles;
            try
            {
                vehicles = await _vehicleRepository.GetAllAsync();
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de vehículos");
                outputPort.StandardHandle(
                    new GetVehiclesUseCaseOutput
                    {
                        Success = false,
                        Message = "Error al obtener la lista de vehículos"
                    },
                    500);
                return;
            }

            if (vehicles == null || vehicles.Count == 0)
            {
                notFoundPort.NotFoundHandle("No se encontraron vehículos.");
                return;
            }

            var output = new GetVehiclesUseCaseOutput
            {
                Vehicles = _mapper.Map<List<VehicleDto>>(vehicles),
                Success = true,
                Message = "Vehículos obtenidos correctamente."
            };
            outputPort.StandardHandle(output);
        }
    }
}
