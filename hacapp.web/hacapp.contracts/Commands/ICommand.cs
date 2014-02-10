namespace hacapp.contracts.Commands
{
    /// <summary>
    ///     Interface which represents a command which can be executed and returns nothing.
    /// </summary>
    /// <remarks>
    ///     The command will represent only the data needed to execute a command.
    ///     The actual logic to execute a command is encapsulated in a command handler, of which there should be one per
    ///     command.
    /// </remarks>
    public interface ICommand
    {
    }
}