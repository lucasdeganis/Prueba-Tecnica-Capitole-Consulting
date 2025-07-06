using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure
{
    public class TestValidationPort : IOutputPortValidation
    {
        public bool WasCalled { get; private set; }

        public string Message { get; private set; }

        public void ValidationHandle(string message)
        {
            WasCalled = true;
            Message = message;
        }
    }
}
