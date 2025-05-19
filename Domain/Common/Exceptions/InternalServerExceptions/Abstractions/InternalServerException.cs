using System.Net;
using Common.Exceptions.Abstractions;

namespace Common.Exceptions.InternalServerExceptions.Abstractions;

public class InternalServerException : VibeNoteException
{
    protected InternalServerException(string message): base(message)
    {
    }

    public override HttpStatusCode Code => HttpStatusCode.InternalServerError;
}