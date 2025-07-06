using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases
{
    public class VehiclePresenter<T> : IWebApiPresenter, IOutputPortStandard<T>, IOutputPortNotFound, IOutputPortValidation
            where T : IUseCaseOutput
    {
        public IActionResult ActionResult { get; private set; }

        public void ValidationHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(new { Message = message });
        }

        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(new { Message = message });
        }

        public void StandardHandle(T response)
        {
            ActionResult = new ObjectResult(response);
        }

        public void StandardHandle(T response, int? status)
        {
            ActionResult = new ObjectResult(response)
            {
                StatusCode = status ?? 200
            };
        }
    }
}
