using System.Net;
using Common.Exceptions.Abstractions;

namespace Common.Exceptions.BadRequestExceptions.Abstractions;

public class BadRequestException : VibeNoteException
{
    protected BadRequestException(string message): base(message)
    {
    }

    public override HttpStatusCode Code => HttpStatusCode.BadRequest;
}