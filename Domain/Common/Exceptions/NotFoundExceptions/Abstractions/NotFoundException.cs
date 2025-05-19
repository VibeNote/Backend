using System.Net;
using Common.Exceptions.Abstractions;

namespace Common.Exceptions.NotFoundExceptions.Abstractions;

public class NotFoundException : VibeNoteException
{
    protected NotFoundException(string message): base(message)
    {
    }

    public override HttpStatusCode Code => HttpStatusCode.NotFound;
}