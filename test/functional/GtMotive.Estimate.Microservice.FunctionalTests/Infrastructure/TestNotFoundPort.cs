using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure
{
    public class TestNotFoundPort : IOutputPortNotFound
    {
        public bool WasCalled { get; private set; }

        public string Message { get; private set; }

        public void NotFoundHandle(string message)
        {
            WasCalled = true;
            Message = message;
        }
    }
}
