namespace EmergingBooking.Infrastructure.CQRS.Command
{
    internal static class InternalErrorMessages
    {
        public static readonly string ErrorObjectIsNotProvidedForFailure = "You attempted to create a failure result.";

        public static readonly string ErrorObjectIsProvidedForSucess = "You attempted to create a sucess result.";
    }

    public class CommandResult
    {
        private static readonly CommandResult OkResult = new CommandResult(true, Enumerable.Empty<string>());

        public CommandResult(bool isSucess, IEnumerable<string> errorMessages)
        {
            bool doNotExistsErrorMessage = errorMessages.Count() == 0;
            bool doExistsErrorMessage = !doNotExistsErrorMessage;

            if (isSucess)
            {
                if (doExistsErrorMessage)                
                    throw new ArgumentException(InternalErrorMessages.ErrorObjectIsProvidedForSucess, nameof(errorMessages));                
            }
            else
            {
                if (doNotExistsErrorMessage)                
                    throw new ArgumentNullException(nameof(errorMessages), InternalErrorMessages.ErrorObjectIsNotProvidedForFailure);
            }

            Success = isSucess;
            ErrorMessages = errorMessages;
        }

        public bool Success { get; }
        public IEnumerable<string> ErrorMessages { get; }
        public bool Failure => !Success;

        public static CommandResult Ok()
        {
            return OkResult;
        }

        public static CommandResult Fail(string errorMessage)
        {
            return new CommandResult(false, new List<string> { errorMessage });
        }

        public static CommandResult Fail(IEnumerable<string> errorMessage)
        {
            return new CommandResult(false, errorMessage);
        }
    }    
}
