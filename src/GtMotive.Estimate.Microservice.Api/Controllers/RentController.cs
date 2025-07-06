using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rent;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    [ApiController]
    [Route("api/rent")]
    public class RentController : ControllerBase
    {
        private readonly IUseCaseWithPresenter<GetRentsUseCaseInput, GetRentsUseCaseOutput> _getRentsUseCase;
        private readonly IUseCaseWithPresenter<RentVehicleUseCaseInput, RentVehicleUseCaseOutput> _rentVehicleUseCase;
        private readonly IUseCaseWithPresenter<ReturnVehicleUseCaseInput, ReturnVehicleUseCaseOutput> _returnVehicleUseCase;

        private readonly RentPresenter<GetRentsUseCaseOutput> _getRentsPresenter;
        private readonly RentPresenter<RentVehicleUseCaseOutput> _rentVehiclePresenter;
        private readonly RentPresenter<ReturnVehicleUseCaseOutput> _returnVehiclePresenter;

        public RentController(
            IUseCaseWithPresenter<GetRentsUseCaseInput, GetRentsUseCaseOutput> getRentsUseCase,
            IUseCaseWithPresenter<RentVehicleUseCaseInput, RentVehicleUseCaseOutput> rentVehicleUseCase,
            IUseCaseWithPresenter<ReturnVehicleUseCaseInput, ReturnVehicleUseCaseOutput> returnVehicleUseCase,
            RentPresenter<GetRentsUseCaseOutput> getRentsPresenter,
            RentPresenter<RentVehicleUseCaseOutput> rentVehiclePresenter,
            RentPresenter<ReturnVehicleUseCaseOutput> returnVehiclePresenter)
        {
            _getRentsUseCase = getRentsUseCase;
            _rentVehicleUseCase = rentVehicleUseCase;
            _returnVehicleUseCase = returnVehicleUseCase;
            _getRentsPresenter = getRentsPresenter;
            _rentVehiclePresenter = rentVehiclePresenter;
            _returnVehiclePresenter = returnVehiclePresenter;
        }

        [HttpGet]
        public async Task<IActionResult> GetRents()
        {
            await _getRentsUseCase.Execute(new GetRentsUseCaseInput(), _getRentsPresenter, _getRentsPresenter, _getRentsPresenter);
            return _getRentsPresenter.ActionResult;
        }

        [HttpPost]
        public async Task<IActionResult> RentVehicle([FromBody] RentVehicleUseCaseInput input)
        {
            await _rentVehicleUseCase.Execute(input, _rentVehiclePresenter, _rentVehiclePresenter, _rentVehiclePresenter);
            return _rentVehiclePresenter.ActionResult;
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnVehicle([FromBody] ReturnVehicleUseCaseInput input)
        {
            await _returnVehicleUseCase.Execute(input, _returnVehiclePresenter, _returnVehiclePresenter, _returnVehiclePresenter);
            return _returnVehiclePresenter.ActionResult;
        }
    }
}
