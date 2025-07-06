using System;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Dto;
using GtMotive.Estimate.Microservice.Domain.Infrastructure;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Fleet
{
    /// <summary>
    /// Este caso de uso permite obtener los detalles de una flota específica por su identificador único.
    /// </summary>
    public class GetFleetUseCase : IUseCaseWithPresenter<GetFleetUseCaseInput, GetFleetUseCaseOutput>
    {
        private readonly IAppLogger<GetFleetUseCase> _logger;
        private readonly IMapper _mapper;
        private readonly IFleetRepository _fleetRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFleetUseCase"/> class.
        /// Este constructor inicializa las dependencias necesarias para el caso de uso de obtención de flota.
        /// </summary>
        /// <param name="fleetRepository">Debe recibir el repositorio de flotas para la gestion de datos.</param>
        /// <param name="logger">Debe recibir el logger para registrar eventos y errores.</param>
        /// <param name="mapper">Debe recibir mapper para gestionar las conversiones de entidades y Dto.</param>
        /// <exception cref="ArgumentNullException">Debuelve una exepcion si las dependencias son null.</exception>
        public GetFleetUseCase(IFleetRepository fleetRepository, IAppLogger<GetFleetUseCase> logger, IMapper mapper)
        {
            _fleetRepository = fleetRepository ?? throw new ArgumentNullException(nameof(fleetRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Este método ejecuta el caso de uso de obtención de una flota por su identificador único.
        /// </summary>
        /// <param name="input">Recibe como dato de entrada el identificador unico de la flota.</param>
        /// <param name="outputPort">Retorna un status en true si se devuelve una flota.</param>
        /// <param name="notFoundPort">Retorna un mensaje si no se encontro algun recurso.</param>
        /// <param name="validationPort">Puerto de salida para manejar validaciones de negocio.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task Execute(GetFleetUseCaseInput input, IOutputPortStandard<GetFleetUseCaseOutput> outputPort, IOutputPortNotFound notFoundPort, IOutputPortValidation validationPort)
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

            Domain.Entities.Fleet fleet;
            try
            {
                fleet = await _fleetRepository.GetByIdAsync(input.FleetId);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Error al obtener la flota desde la base de datos.");
                outputPort.StandardHandle(
                    new GetFleetUseCaseOutput
                    {
                        Success = false,
                        Message = $"Error al obtener la flota: {ex.Message}"
                    },
                    500);
                return;
            }

            if (fleet == null)
            {
                notFoundPort.NotFoundHandle("No se encontro la flota.");
                return;
            }

            var output = new GetFleetUseCaseOutput
            {
                Fleet = _mapper.Map<FleetDto>(fleet),
                Success = true,
                Message = "Se encontro la flotas."
            };

            outputPort.StandardHandle(output);
        }
    }
}
