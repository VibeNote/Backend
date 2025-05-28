using System.Net;

namespace Common.Exceptions.Abstractions;

public class VibeNoteException : Exception
{
    protected VibeNoteException(string message): base(message)
    {
    }

    public virtual HttpStatusCode Code { get; } = HttpStatusCode.InternalServerError;
}