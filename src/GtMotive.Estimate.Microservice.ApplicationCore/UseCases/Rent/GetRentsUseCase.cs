using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Dto;
using GtMotive.Estimate.Microservice.Domain.Infrastructure;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rent
{
    /// <summary>
    /// Este caso de uso permite obtener los alquileres de vehículos registrados en el sistema.
    /// </summary>
    public class GetRentsUseCase : IUseCaseWithPresenter<GetRentsUseCaseInput, GetRentsUseCaseOutput>
    {
        private readonly IAppLogger<RentVehicleUseCase> _logger;
        private readonly IMapper _mapper;
        private readonly IRentRepository _rentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRentsUseCase"/> class.
        /// Este constructor inicializa las dependencias necesarias para el caso de uso de obtención de alquileres.
        /// </summary>
        /// <param name="rentRepository">Debe recibir el repositorio de alquiler para la gestion de datos.</param>
        /// <param name="logger">Debe recibir el logger para registrar eventos y errores.</param>
        /// <param name="mapper">Debe recibir mapper para gestionar las conversiones de entidades y Dto.</param>
        /// <exception cref="ArgumentNullException">Lanza una excepción si alguno de los repositorios es nulo.</exception>
        public GetRentsUseCase(IRentRepository rentRepository, IAppLogger<RentVehicleUseCase> logger, IMapper mapper)
        {
            _rentRepository = rentRepository ?? throw new ArgumentNullException(nameof(rentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Este método ejecuta el caso de uso de obtención de alquileres.
        /// </summary>
        /// <param name="input">En este caso no recive ningun parametro de entrada.</param>
        /// <param name="outputPort">Retorna un status en true junto a la lista de alquileres.</param>
        /// <param name="notFoundPort">Retorna un mensaje si no se encontro algun recurso.</param>
        /// <param name="validationPort">Puerto de salida para manejar validaciones de negocio.</param>
        /// <exception cref="ArgumentNullException">Lanza una excepción si alguno de los puertos es nulo.</exception>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task Execute(GetRentsUseCaseInput input, IOutputPortStandard<GetRentsUseCaseOutput> outputPort, IOutputPortNotFound notFoundPort, IOutputPortValidation validationPort)
        {
            ArgumentNullException.ThrowIfNull(outputPort);
            ArgumentNullException.ThrowIfNull(notFoundPort);
            ArgumentNullException.ThrowIfNull(validationPort);

            try
            {
                var rents = await _rentRepository.GetAllAsync();

                if (rents == null || rents.Count == 0)
                {
                    notFoundPort.NotFoundHandle("No se encontraron alquileres.");
                    return;
                }

                var output = new GetRentsUseCaseOutput
                {
                    Success = true,
                    Message = "Alquileres obtenidos correctamente.",
                    Rents = _mapper.Map<List<RentDto>>(rents)
                };

                outputPort.StandardHandle(output);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Error al obtener los alquileres desde la base de datos.");
                outputPort.StandardHandle(
                    new GetRentsUseCaseOutput
                    {
                        Success = false,
                        Message = "Error al obtener los alquileres."
                    },
                    500);
            }
        }
    }
}
