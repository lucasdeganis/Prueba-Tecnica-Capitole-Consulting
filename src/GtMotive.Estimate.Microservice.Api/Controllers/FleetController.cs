using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Fleet;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    [ApiController]
    [Route("api/fleet")]
    public class FleetController : ControllerBase
    {
        private readonly IUseCaseWithPresenter<GetFleetUseCaseInput, GetFleetUseCaseOutput> _getFleetUseCase;
        private readonly IUseCaseWithPresenter<GetFleetsUseCaseInput, GetFleetsUseCaseOutput> _getFleetsUseCase;
        private readonly IUseCaseWithPresenter<GetVehiclesByFleetUseCaseInput, GetVehiclesByFleetUseCaseOutput> _getVehiclesByFleetUseCase;
        private readonly IUseCaseWithPresenter<AddVehicleToFleetUseCaseInput, AddVehicleToFleetUseCaseOutput> _addVehicleToFleetUseCase;

        private readonly FleetPresenter<GetFleetUseCaseOutput> _getFleetPresenter;
        private readonly FleetPresenter<GetFleetsUseCaseOutput> _getFleetsPresenter;
        private readonly FleetPresenter<GetVehiclesByFleetUseCaseOutput> _getVehiclesByFleetPresenter;
        private readonly FleetPresenter<AddVehicleToFleetUseCaseOutput> _addVehicleToFleetPresenter;

        public FleetController(
            IUseCaseWithPresenter<GetFleetUseCaseInput, GetFleetUseCaseOutput> getFleetUseCase,
            IUseCaseWithPresenter<GetFleetsUseCaseInput, GetFleetsUseCaseOutput> getFleetsUseCase,
            IUseCaseWithPresenter<GetVehiclesByFleetUseCaseInput, GetVehiclesByFleetUseCaseOutput> getVehiclesByFleetUseCase,
            IUseCaseWithPresenter<AddVehicleToFleetUseCaseInput, AddVehicleToFleetUseCaseOutput> addVehicleToFleetUseCase,
            FleetPresenter<GetFleetUseCaseOutput> getFleetPresenter,
            FleetPresenter<GetFleetsUseCaseOutput> getFleetsPresenter,
            FleetPresenter<GetVehiclesByFleetUseCaseOutput> getVehiclesByFleetPresenter,
            FleetPresenter<AddVehicleToFleetUseCaseOutput> addVehicleToFleetPresenter)
        {
            _getFleetUseCase = getFleetUseCase;
            _getFleetsUseCase = getFleetsUseCase;
            _getVehiclesByFleetUseCase = getVehiclesByFleetUseCase;
            _addVehicleToFleetUseCase = addVehicleToFleetUseCase;

            _getFleetPresenter = getFleetPresenter;
            _getFleetsPresenter = getFleetsPresenter;
            _getVehiclesByFleetPresenter = getVehiclesByFleetPresenter;
            _addVehicleToFleetPresenter = addVehicleToFleetPresenter;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFleet(Guid id)
        {
            await _getFleetUseCase.Execute(new GetFleetUseCaseInput { FleetId = id }, _getFleetPresenter, _getFleetPresenter, _getFleetPresenter);
            return _getFleetPresenter.ActionResult;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetFleets()
        {
            await _getFleetsUseCase.Execute(new GetFleetsUseCaseInput(), _getFleetsPresenter, _getFleetsPresenter, _getFleetsPresenter);
            return _getFleetsPresenter.ActionResult;
        }

        [HttpGet("{id}/vehicles")]
        public async Task<IActionResult> GetVehiclesByFleet(Guid id)
        {
            await _getVehiclesByFleetUseCase.Execute(new GetVehiclesByFleetUseCaseInput { FleetId = id }, _getVehiclesByFleetPresenter, _getVehiclesByFleetPresenter, _getVehiclesByFleetPresenter);
            return _getVehiclesByFleetPresenter.ActionResult;
        }

        [HttpPost("addVehicleToFleet")]
        public async Task<IActionResult> AddVehicleToFleet([FromBody] AddVehicleToFleetUseCaseInput input)
        {
            await _addVehicleToFleetUseCase.Execute(input, _addVehicleToFleetPresenter, _addVehicleToFleetPresenter, _addVehicleToFleetPresenter);
            return _addVehicleToFleetPresenter.ActionResult;
        }
    }
}
