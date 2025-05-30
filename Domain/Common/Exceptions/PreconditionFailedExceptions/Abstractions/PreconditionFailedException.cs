using System.Net;
using Common.Exceptions.Abstractions;

namespace Common.Exceptions.PreconditionFailedExceptions.Abstractions;

public class PreconditionFailedException : VibeNoteException
{
    protected PreconditionFailedException(string message): base(message)
    {
    }

    public override HttpStatusCode Code => HttpStatusCode.PreconditionFailed;
}