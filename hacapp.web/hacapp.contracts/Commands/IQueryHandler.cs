using System;
using System.Threading.Tasks;

namespace hacapp.contracts.Commands
{
    public interface IQueryHandler<in TQuery, out TResult> : IQueryHandler where TQuery : IQuery<TResult>
    {
        /// <summary>
        ///     Executes the query and returns the results.  Will throw an exception if the command could not be executed for any
        ///     reason.
        /// </summary>
        TResult Execute(TQuery query, ICommandAndQueryDispatcher dispatcher);
    }

    /// <summary>
    ///     Genericless instance of the IQueryHandler interface.  This exists so that collections of IQueryHandlers can be
    ///     created and so we can dispatch the queries at runtime based on the type of the query
    /// </summary>
    public interface IQueryHandler
    {
        /// <summary>
        ///     Executes the query and returns the results.  Will throw an exception if the command could not be executed for any
        ///     reason.
        /// </summary>
        /// <returns>
        ///     The return type of this method will always be a task whose result will be of the type of the result of the
        ///     query
        /// </returns>
        object Execute(object query, ICommandAndQueryDispatcher dispatcher);
    }
}