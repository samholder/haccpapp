namespace Hacapp.Core.Commands
{
    public class CommandExecutionExceptionMessages
    {
        public static string TeamDoesNotExist(int teamId)
        {
            return string.Format("The team with the id {0} does not exist", teamId);
        }
    }
}