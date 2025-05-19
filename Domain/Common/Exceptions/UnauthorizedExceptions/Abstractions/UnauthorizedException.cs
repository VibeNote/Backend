using System.Net;
using Common.Exceptions.Abstractions;

namespace Common.Exceptions.UnauthorizedExceptions.Abstractions;

public class UnauthorizedException : VibeNoteException
{
    protected UnauthorizedException(string message): base(message)
    {
    }

    public override HttpStatusCode Code => HttpStatusCode.Unauthorized;
}