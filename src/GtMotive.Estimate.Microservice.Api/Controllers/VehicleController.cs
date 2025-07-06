using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehicleController : ControllerBase
    {
        private readonly IUseCaseWithPresenter<GetVehiclesUseCaseInput, GetVehiclesUseCaseOutput> _getVehiclesUseCase;
        private readonly IUseCaseWithPresenter<GetVehicleUseCaseInput, GetVehicleUseCaseOutput> _getVehicleUseCase;
        private readonly IUseCaseWithPresenter<CreateVehicleUseCaseInput, CreateVehicleUseCaseOutput> _createVehicleUseCase;
        private readonly VehiclePresenter<GetVehiclesUseCaseOutput> _getVehiclesPresenter;
        private readonly VehiclePresenter<GetVehicleUseCaseOutput> _getVehiclePresenter;
        private readonly VehiclePresenter<CreateVehicleUseCaseOutput> _createVehiclePresenter;

        public VehicleController(
            IUseCaseWithPresenter<GetVehiclesUseCaseInput, GetVehiclesUseCaseOutput> getVehiclesUseCase,
            IUseCaseWithPresenter<GetVehicleUseCaseInput, GetVehicleUseCaseOutput> getVehicleUseCase,
            IUseCaseWithPresenter<CreateVehicleUseCaseInput, CreateVehicleUseCaseOutput> createVehicleUseCase,
            VehiclePresenter<GetVehiclesUseCaseOutput> getVehiclesPresenter,
            VehiclePresenter<GetVehicleUseCaseOutput> getVehiclePresenter,
            VehiclePresenter<CreateVehicleUseCaseOutput> createVehiclePresenter)
        {
            _getVehiclesUseCase = getVehiclesUseCase;
            _getVehicleUseCase = getVehicleUseCase;
            _createVehicleUseCase = createVehicleUseCase;
            _getVehiclesPresenter = getVehiclesPresenter;
            _getVehiclePresenter = getVehiclePresenter;
            _createVehiclePresenter = createVehiclePresenter;
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            await _getVehiclesUseCase.Execute(new GetVehiclesUseCaseInput(), _getVehiclesPresenter, _getVehiclesPresenter, _getVehiclesPresenter);
            return _getVehiclesPresenter.ActionResult;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(Guid id)
        {
            await _getVehicleUseCase.Execute(new GetVehicleUseCaseInput { Id = id }, _getVehiclePresenter, _getVehiclePresenter, _getVehiclePresenter);
            return _getVehiclePresenter.ActionResult;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleUseCaseInput input)
        {
            await _createVehicleUseCase.Execute(input, _createVehiclePresenter, _createVehiclePresenter, _createVehiclePresenter);
            return _createVehiclePresenter.ActionResult;
        }
    }
}
