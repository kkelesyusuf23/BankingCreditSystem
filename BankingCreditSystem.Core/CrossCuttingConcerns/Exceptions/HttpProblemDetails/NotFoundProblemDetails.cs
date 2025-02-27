namespace BankingCreditSystem.Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class NotFoundProblemDetails : ProblemDetails
{
    public NotFoundProblemDetails(string detail)
    {
        Title = "Not found";
        Detail = detail;
        Status = StatusCodes.Status404NotFound;
        Type = "https://example.com/probs/notfound";
    }
} 