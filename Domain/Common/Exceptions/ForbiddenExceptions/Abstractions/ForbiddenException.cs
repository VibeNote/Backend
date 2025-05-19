using System.Net;
using Common.Exceptions.Abstractions;

namespace Common.Exceptions.ForbiddenExceptions.Abstractions;

public class ForbiddenException : VibeNoteException
{
    protected ForbiddenException(string message): base(message)
    {
    }

    public override HttpStatusCode Code => HttpStatusCode.Forbidden;
}
