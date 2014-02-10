using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using hacapp.contracts.Commands;
using hacapp.core.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Haccapp.Core.Tests.Commands
{
    [TestClass]
    public class CommandAndQueryDispatcherTests
    {
        [TestClass]
        public class TheExecuteMethod
        {
            [TestMethod]
            public void ShouldUseRegisteredCommandHandlerToExecuteCommand()
            {
                List<IQueryHandler> queryHandlers =
                    Enumerable.Repeat<IQueryHandler>(new TestQueryHandler1<TestQuery1, string>(string.Empty), 1)
                        .ToList();
                var mockHandler = new TestCommandHandler1<TestCommand1>();
                ICollection<ICommandHandler> commandHandlers = new List<ICommandHandler>
                {
                    mockHandler
                };
                var command = new TestCommand1();

                var dispatcher = new CommandAndQueryDispatcher(commandHandlers, queryHandlers);
                dispatcher.ExecuteCommand(command);
                mockHandler.CommandExecuted.Should().Be(command);
            }

            [TestMethod]
            public void ShouldUseRegisteredCommandHandlerToExecuteCommandWhenMultipelHandlerExist()
            {
                List<IQueryHandler> queryHandlers =
                    Enumerable.Repeat<IQueryHandler>(new TestQueryHandler1<TestQuery1, string>(string.Empty), 1)
                        .ToList();
                var mockHandler = new TestCommandHandler1<TestCommand1>();
                var mockHandler2 = new TestCommandHandler2<TestCommand2>();
                ICollection<ICommandHandler> commandHandlers = new List<ICommandHandler>
                {
                    mockHandler,
                    mockHandler2
                };
                var command = new TestCommand1();

                var dispatcher = new CommandAndQueryDispatcher(commandHandlers, queryHandlers);
                dispatcher.ExecuteCommand(command);
                mockHandler.CommandExecuted.Should().Be(command);
                mockHandler2.CommandExecuted.Should().BeNull();
            }

            [TestMethod]
            public void ShouldThrowInvalidOperationExceptionWhenNoCommandHandlerIsRegistered()
            {
                List<IQueryHandler> queryHandlers =
                    Enumerable.Repeat<IQueryHandler>(new TestQueryHandler1<TestQuery1, string>(string.Empty), 1)
                        .ToList();
                var mockHandler = new TestCommandHandler1<TestCommand1>();
                ICollection<ICommandHandler> commandHandlers = new List<ICommandHandler>
                {
                    mockHandler,
                };
                var command = new TestCommand2();

                var dispatcher = new CommandAndQueryDispatcher(commandHandlers, queryHandlers);
                Action action = () => dispatcher.ExecuteCommand(command);
                action.ShouldThrow<InvalidOperationException>();
            }

            [TestMethod]
            public void ShouldUseRegisteredQueryHandlerToExecuteQuery()
            {
                const string expectedResult = "expectedResult";
                var mockHandler = new TestQueryHandler1<TestQuery1, string>(expectedResult);
                ICollection<ICommandHandler> commandHandlers =
                    Enumerable.Repeat<ICommandHandler>(new TestCommandHandler1<TestCommand1>(), 1).ToList();
                ICollection<IQueryHandler> queryHandlers = new List<IQueryHandler>
                {
                    mockHandler
                };
                var query = new TestQuery1();

                var dispatcher = new CommandAndQueryDispatcher(commandHandlers, queryHandlers);
                string queryResult = dispatcher.ExecuteQuery(query);
                mockHandler.QueryExecuted.Should().Be(query);
                queryResult.Should().Be(expectedResult);
            }

            [TestMethod]
            public void ShouldUseRegisteredQueryHandlerToExecuteQueryWhenMultiple()
            {
                const string expectedResult = "expectedResult";
                const string expectedResult2 = "expectedResult2";
                var mockHandler = new TestQueryHandler1<TestQuery1, string>(expectedResult);
                var mockHandler2 = new TestQueryHandler2<TestQuery2, string>(expectedResult2);
                ICollection<ICommandHandler> commandHandlers =
                    Enumerable.Repeat<ICommandHandler>(new TestCommandHandler1<TestCommand1>(), 1).ToList();
                ICollection<IQueryHandler> queryHandlers = new List<IQueryHandler>
                {
                    mockHandler,
                    mockHandler2
                };
                var query = new TestQuery2();

                var dispatcher = new CommandAndQueryDispatcher(commandHandlers, queryHandlers);
                string queryResult = dispatcher.ExecuteQuery(query);
                mockHandler2.QueryExecuted.Should().Be(query);
                queryResult.Should().Be(expectedResult2);
                mockHandler.QueryExecuted.Should().BeNull();
            }

            [TestMethod]
            public void ShouldThrowInvalidOperationExceptionWhenQueryHandlerIsNotRegistered()
            {
                const string expectedResult = "expectedResult";
                const string expectedResult2 = "expectedResult2";
                var mockHandler = new TestQueryHandler1<TestQuery1, string>(expectedResult);
                ICollection<ICommandHandler> commandHandlers =
                    Enumerable.Repeat<ICommandHandler>(new TestCommandHandler1<TestCommand1>(), 1).ToList();
                ICollection<IQueryHandler> queryHandlers = new List<IQueryHandler>
                {
                    mockHandler,
                };
                var query = new TestQuery2();

                var dispatcher = new CommandAndQueryDispatcher(commandHandlers, queryHandlers);
                string queryResult;
                Action action = () => queryResult = dispatcher.ExecuteQuery(query);
                action.ShouldThrow<InvalidOperationException>();
            }

            private class TestQuery1 : IQuery<string>
            {
            }

            private class TestQuery2 : IQuery<string>
            {
            }

            private class TestQueryHandler1<T, TResult> : QueryHandlerBase<T, TResult> where T : IQuery<TResult>
            {
                private readonly TResult expectedResult;

                public TestQueryHandler1(TResult expectedResult)
                {
                    this.expectedResult = expectedResult;
                }

                public T QueryExecuted { get; private set; }

                public override TResult Execute(T query, ICommandAndQueryDispatcher dispatcher)
                {
                    QueryExecuted = query;
                    return expectedResult;
                }
            }

            private class TestQueryHandler2<T, TResult> : QueryHandlerBase<T, TResult> where T : IQuery<TResult>
            {
                private readonly TResult expectedResult;

                public TestQueryHandler2(TResult expectedResult)
                {
                    this.expectedResult = expectedResult;
                }

                public T QueryExecuted { get; private set; }

                public override TResult Execute(T query, ICommandAndQueryDispatcher dispatcher)
                {
                    QueryExecuted = query;
                    return expectedResult;
                }
            }

            #region TestCommands/Handlers

            private class TestCommand1 : ICommand
            {
            }

            private class TestCommand2 : ICommand
            {
            }

            private class TestCommandHandler1<T> : CommandHandlerBase<T> where T : ICommand
            {
                private T commandExecuted;

                public T CommandExecuted
                {
                    get { return commandExecuted; }
                }

                public override void Execute(T command, ICommandAndQueryDispatcher dispatcher)
                {
                    commandExecuted = command;
                }
            }

            private class TestCommandHandler2<T> : CommandHandlerBase<T> where T : ICommand
            {
                private T commandExecuted;

                public T CommandExecuted
                {
                    get { return commandExecuted; }
                }

                public override void Execute(T command, ICommandAndQueryDispatcher dispatcher)
                {
                    commandExecuted = command;
                }
            }

            #endregion
        }
    }
}