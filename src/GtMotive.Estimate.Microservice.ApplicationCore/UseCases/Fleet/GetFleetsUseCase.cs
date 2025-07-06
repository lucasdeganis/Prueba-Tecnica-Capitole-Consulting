using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Dto;
using GtMotive.Estimate.Microservice.Domain.Infrastructure;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Fleet
{
    /// <summary>
    /// Este caso de uso permite obtener todas las flotas disponibles en el sistema.
    /// </summary>
    public class GetFleetsUseCase : IUseCaseWithPresenter<GetFleetsUseCaseInput, GetFleetsUseCaseOutput>
    {
        private readonly IAppLogger<GetFleetsUseCase> _logger;
        private readonly IMapper _mapper;
        private readonly IFleetRepository _fleetRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFleetsUseCase"/> class.
        /// Este constructor inicializa las dependencias necesarias para el caso de uso de obtención de flotas.
        /// </summary>
        /// <param name="fleetRepository">Debe recibir el repositorio de flotas para la gestion de datos.</param>
        /// <param name="logger">Debe recibir el logger para registrar eventos y errores.</param>
        /// <param name="mapper">Debe recibir mapper para gestionar las conversiones de entidades y Dto.</param>
        /// <exception cref="ArgumentNullException">Debuelve una exepcion si las dependencias son null.</exception>
        public GetFleetsUseCase(IFleetRepository fleetRepository, IAppLogger<GetFleetsUseCase> logger, IMapper mapper)
        {
            _fleetRepository = fleetRepository ?? throw new ArgumentNullException(nameof(fleetRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Este método ejecuta el caso de uso de obtención de todas las flotas disponibles.
        /// </summary>
        /// <param name="input">No recibe nada como parametro ya que no se deficinio que pueda enviar filtros.</param>
        /// <param name="outputPort">Retorna un status en true si se devuelve una lista de flotas.</param>
        /// <param name="notFoundPort">Retorna un mensaje si no se encontro algun recurso.</param>
        /// <param name="validationPort">Puerto de salida para manejar validaciones de negocio.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task Execute(GetFleetsUseCaseInput input, IOutputPortStandard<GetFleetsUseCaseOutput> outputPort, IOutputPortNotFound notFoundPort, IOutputPortValidation validationPort)
        {
            ArgumentNullException.ThrowIfNull(outputPort);
            ArgumentNullException.ThrowIfNull(notFoundPort);
            ArgumentNullException.ThrowIfNull(validationPort);

            if (input == null)
            {
                validationPort.ValidationHandle("El input no puede ser nulo.");
                return;
            }

            List<Domain.Entities.Fleet> fleets;
            try
            {
                fleets = await _fleetRepository.GetAllAsync();
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Error al obtener las flotas desde la base de datos.");
                outputPort.StandardHandle(
                    new GetFleetsUseCaseOutput
                    {
                        Success = false,
                        Message = $"Error al obtener las flotas: {ex.Message}"
                    },
                    500);
                return;
            }

            if (fleets == null || fleets.Count == 0)
            {
                notFoundPort.NotFoundHandle("No se encontraron flotas.");
                return;
            }

            var output = new GetFleetsUseCaseOutput
            {
                Fleets = _mapper.Map<List<FleetDto>>(fleets),
                Success = true,
                Message = "Se encontraron flotas."
            };

            outputPort.StandardHandle(output);
        }
    }
}
