namespace hacapp.contracts.Commands
{
    /// <summary>
    /// Interface for a query which has a result.
    /// </summary>
    /// <typeparam name="TResult">The type of the result of the execution of the query.</typeparam>
    public interface IQuery<out TResult>
    {
    }
}