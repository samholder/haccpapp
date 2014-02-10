using System.Threading.Tasks;

namespace hacapp.contracts.Commands
{
    /// <summary>
    ///     Base implementation for the generic version of IQueryHandler interface which handlers the non generic version of
    ///     the execute method
    /// </summary>
    /// <typeparam name="TQuery">The type of the Query that the derived handler will handle.</typeparam>
    /// <typeparam name="TResult">The type of the result of the query that the derived handler will handle.</typeparam>
    public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        public object Execute(object query, ICommandAndQueryDispatcher dispatcher)
        {
            return Execute((TQuery) query, dispatcher);
        }

        public abstract TResult Execute(TQuery query, ICommandAndQueryDispatcher dispatcher);
    }
}