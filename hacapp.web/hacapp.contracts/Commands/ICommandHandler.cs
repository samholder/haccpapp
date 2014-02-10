using System.Threading.Tasks;

namespace hacapp.contracts.Commands
{
    /// <summary>
    ///     Interface for objects which will handle commands
    /// </summary>
    /// <typeparam name="TCommand">The type of the command the the implementation will handle</typeparam>
    public interface ICommandHandler<in TCommand> : ICommandHandler where TCommand : ICommand
    {
        /// <summary>
        ///     Executes the command.  Will throw an exception if the command could not be executed for any reason.
        /// </summary>
        void Execute(TCommand command, ICommandAndQueryDispatcher dispatcher);
    }

    /// <summary>
    ///     Genericless definition of the command handler to allow us to register the commands without knowing what the type of
    ///     command they handle is
    /// </summary>
    public interface ICommandHandler
    {
        /// <summary>
        ///     Executes the command.  Will throw an exception if the command could not be executed for any reason.
        /// </summary>
        /// <returns>The return type of this method will always be a task</returns>
        void Execute(object command, ICommandAndQueryDispatcher dispatcher);
    }
}