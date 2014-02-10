using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using hacapp.contracts.Commands;

namespace hacapp.core.Commands
{
    /// <summary>
    ///     The default implemenation of the ICommandAndQueryDispatcher interface.
    /// </summary>
    /// <remarks>
    ///     Given a command or query this implementation looks up the appropriate handler for that command or query and
    ///     then invokes the command or query.
    /// </remarks>
    public class CommandAndQueryDispatcher : ICommandAndQueryDispatcher
    {
        private readonly IDictionary<Type, ICommandHandler> commandHandlers = new Dictionary<Type, ICommandHandler>();
        private readonly IDictionary<Type, IQueryHandler> queryHandlers = new Dictionary<Type, IQueryHandler>();

        public CommandAndQueryDispatcher(IEnumerable<ICommandHandler> commandHandlers,
            IEnumerable<IQueryHandler> queryHandlers)
        {
            commandHandlers = commandHandlers.ToList();
            if (!commandHandlers.Any())
            {
                throw new ArgumentException("No command handlers registered");
            }

            queryHandlers = queryHandlers.ToList();
            if (!queryHandlers.Any())
            {
                throw new ArgumentException("No query handlers registered");
            }

            foreach (ICommandHandler commandHandler in commandHandlers)
            {
                this.commandHandlers.Add(
                    commandHandler.GetType().GetInterface("ICommandHandler`1").GenericTypeArguments[0], commandHandler);
            }

            foreach (IQueryHandler queryHandler in queryHandlers)
            {
                this.queryHandlers.Add(queryHandler.GetType().GetInterface("IQueryHandler`2").GenericTypeArguments[0],
                    queryHandler);
            }
        }

        [DebuggerStepThrough]
        public void ExecuteCommand(ICommand command)
        {
            ICommandHandler commandHandler;
            if (commandHandlers.TryGetValue(command.GetType(), out commandHandler))
            {
                commandHandler.Execute(command, this);
                return;
            }

            throw new InvalidOperationException(
                "No command handler was found to be registered for the command with the type " + command.GetType());
        }

        [DebuggerStepThrough]
        public TResult ExecuteQuery<TResult>(IQuery<TResult> query)
        {
            IQueryHandler queryHandler;
            if (queryHandlers.TryGetValue(query.GetType(), out queryHandler))
            {
                return (TResult) queryHandler.Execute(query, this);
            }

            throw new InvalidOperationException(
                "No query handler was found to be registered for the query with the type " + query.GetType());
        }
    }
}