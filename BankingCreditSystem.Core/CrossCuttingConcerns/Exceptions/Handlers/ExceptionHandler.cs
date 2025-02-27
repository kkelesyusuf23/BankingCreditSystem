using BankingCreditSystem.Core.CrossCuttingConcerns.Exceptions.Types;

public abstract class ExceptionHandler
{
    public Task HandleExceptionAsync(Exception exception)
    {
        if (exception is BusinessException businessException)
            return HandleException(businessException);

        if (exception is ValidationException validationException)
            return HandleException(validationException);

        if (exception is AuthorizationException authorizationException)
            return HandleException(authorizationException);

        return HandleException(exception);
    }

    protected abstract Task HandleException(BusinessException businessException);
    protected abstract Task HandleException(ValidationException validationException);
    protected abstract Task HandleException(AuthorizationException authorizationException);
    protected abstract Task HandleException(Exception exception);
}