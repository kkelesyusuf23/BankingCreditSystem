using BankingCreditSystem.Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using BankingCreditSystem.Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;

public class HttpExceptionHandler : ExceptionHandler
{
    private HttpResponse? _response;

    public HttpResponse Response
    {
        get => _response ?? throw new ArgumentNullException(nameof(_response));
        set => _response = value;
    }

    protected override Task HandleException(BusinessException businessException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        var details = new BusinessProblemDetails(businessException.Message);
        return WriteAsJsonAsync(Response, details);
    }

    protected override Task HandleException(ValidationException validationException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        var details = new ValidationProblemDetails(validationException.Errors);
        return WriteAsJsonAsync(Response, details);
    }

    protected override Task HandleException(AuthorizationException authorizationException)
    {
        Response.StatusCode = StatusCodes.Status401Unauthorized;
        var details = new AuthorizationProblemDetails(authorizationException.Message);
        return WriteAsJsonAsync(Response, details);
    }

    protected override Task HandleException(Exception exception)
    {
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        var details = new InternalServerErrorProblemDetails(exception.Message);
        return WriteAsJsonAsync(Response, details);
    }

    private static Task WriteAsJsonAsync<T>(HttpResponse response, T value)
    {
        response.ContentType = "application/json";
        return JsonSerializer.SerializeAsync(response.Body, value);
    }
}