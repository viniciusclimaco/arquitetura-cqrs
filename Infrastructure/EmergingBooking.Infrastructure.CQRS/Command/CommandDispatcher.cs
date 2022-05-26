﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingBooking.Infrastructure.CQRS.Command
{
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly DependencyResolver _dependencyResolver;

        public CommandDispatcher(DependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public async Task<CommandResult> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var handler = _dependencyResolver.Resolve<ICommandHandler<TCommand>>();
            return await handler.ExecuteAsync(command);
        }
    }
}
