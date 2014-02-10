namespace hacapp.contracts.Commands
{
    /// <summary>
    ///     Base class for command handlers which handles the genericless implementation of the Execute method
    /// </summary>
    /// <typeparam name="TCommand">The type of the command which the handler can execute</typeparam>
    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public abstract void Execute(TCommand command, ICommandAndQueryDispatcher dispatcher);

        /// <summary>
        ///     This method simply casts the command to the expected type and calls the abstract execute method.
        /// </summary>
        /// <param name="command">The command to execute, whcih should be an object of type ICommand</param>
        /// <param name="dispatcher"></param>
        public void Execute(object command, ICommandAndQueryDispatcher dispatcher)
        {
            Execute((TCommand) command, dispatcher);
        }
    }
}