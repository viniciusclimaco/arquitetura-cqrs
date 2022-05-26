using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingBooking.Infrastructure.CQRS.Command
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<CommandResult> ExecuteAsync(TCommand command);
    }
}
