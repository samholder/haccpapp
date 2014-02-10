namespace hacapp.contracts.Commands
{
    /// <summary>
    ///     Interface which represents a command with a result.
    /// </summary>
    /// <remarks>
    ///     A command which has a result should only be used when creating new data and the result of the command is the
    ///     new key for the object that the command created.
    /// </remarks>
    public interface ICommandWithResult<TResult> : ICommand
    {
        /// <summary>
        ///     The result of executing the command
        /// </summary>
        TResult Result { get; set; }
    }
}