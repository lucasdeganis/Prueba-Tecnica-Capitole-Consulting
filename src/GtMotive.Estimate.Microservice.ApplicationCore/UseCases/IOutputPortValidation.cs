namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases
{
    /// <summary>
    /// Interface to define the Not Found Output Port.
    /// </summary>
    public interface IOutputPortValidation
    {
        /// <summary>
        /// Informs the resource has invalid data.
        /// </summary>
        /// <param name="message">Text description.</param>
        void ValidationHandle(string message);
    }
}
