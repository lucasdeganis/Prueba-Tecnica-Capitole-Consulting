using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure
{
    public class TestOutputPort : IOutputPortStandard<GetVehicleUseCaseOutput>
    {
        public bool WasCalled { get; private set; }

        public GetVehicleUseCaseOutput Output { get; private set; }

        public void StandardHandle(GetVehicleUseCaseOutput output)
        {
            WasCalled = true;
            Output = output;
        }

        public void StandardHandle(GetVehicleUseCaseOutput output, int? status)
        {
            WasCalled = true;
            Output = output;
        }
    }
}
