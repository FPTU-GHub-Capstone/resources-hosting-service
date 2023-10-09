using AutoWrapper.Wrappers;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Extensions;

public class ConflictException : BaseHttpException
{
    private readonly static int statusCode = StatusCodes.Status409Conflict;
    public ConflictException(object customError) : base(customError, statusCode)
    {
    }

    public ConflictException(IEnumerable<ValidationError> errors) : base(errors, statusCode)
    {
    }

    public ConflictException(Exception ex) : base(ex, statusCode)
    {
    }

    public ConflictException(string message = "Conflict", string errorCode = null, string refLink = null) : base(message, statusCode, errorCode, refLink)
    {
    }
}
