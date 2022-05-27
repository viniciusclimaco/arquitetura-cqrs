namespace EmergingBooking.Infrastructure.CQRS.Command
{
    public interface ICommandDispatcher
    {
        Task<CommandResult> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
