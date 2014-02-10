namespace hacapp.contracts.Commands
{
    /// <summary>
    /// Interface which defines the contract for dispatching and executing commands and queries
    /// </summary>
    public interface ICommandAndQueryDispatcher
    {
        /// <summary>
        /// Executes a command
        /// </summary>
        /// <param name="command">The command to execute</param>
        void ExecuteCommand(ICommand command);

        /// <summary>
        /// Executes a query and returns the result
        /// </summary>
        /// <typeparam name="TResult">The type of the result of the query</typeparam>
        /// <param name="query">The query to execute</param>
        /// <returns>The query that we want to execute</returns>
        TResult ExecuteQuery<TResult>(IQuery<TResult> query);
    }
}