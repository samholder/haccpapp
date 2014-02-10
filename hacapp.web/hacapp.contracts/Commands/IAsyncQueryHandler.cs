using System.Threading.Tasks;

namespace hacapp.contracts.Commands
{
    public interface IAsyncQueryHandler<in TQuery, TResult> : IQueryHandler where TQuery : IQuery<TResult>
    {
        /// <summary>
        ///     Executes the query asyncromously and returns the a task which represents the result.  Will throw an exception if the command could not be executed for any
        ///     reason.
        /// </summary>
        Task<TResult> ExecuteAsync(TQuery query);
    }
}