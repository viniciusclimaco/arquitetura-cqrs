namespace EmergingBooking.Infrastructure.CQRS.Command
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<CommandResult> ExecuteAsync(TCommand command);
    }
}
