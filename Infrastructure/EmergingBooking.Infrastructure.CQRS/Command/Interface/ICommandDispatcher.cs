using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingBooking.Infrastructure.CQRS.Command
{
    public interface ICommandDispatcher
    {
        Task<CommandResult> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
