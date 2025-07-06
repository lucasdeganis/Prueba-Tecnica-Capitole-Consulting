using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases
{
    /// <summary>
    /// Interface for the handler of an use case.
    /// </summary>
    /// <typeparam name="TUseCaseInput">Type of the input message.</typeparam>
    /// <typeparam name="TUseCaseOutput">Type of the use case response dto.</typeparam>
    public interface IUseCaseWithPresenter<TUseCaseInput, out TUseCaseOutput>
        where TUseCaseInput : IUseCaseInput
        where TUseCaseOutput : IUseCaseOutput
    {
        /// <summary>
        /// The method to execute the use case.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <param name="input">The input for the use case.</param>
        /// <param name="outputPort">The output port for standard responses.</param>
        /// <param name="notFoundPort">The output port for not found responses.</param>
        /// <param name="validationPort">The output port for validation responses.</param>
        Task Execute(TUseCaseInput input, IOutputPortStandard<TUseCaseOutput> outputPort, IOutputPortNotFound notFoundPort, IOutputPortValidation validationPort);
    }
}
