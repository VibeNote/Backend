using System.Net;
using Common.Exceptions.Abstractions;
using Common.Exceptions.InternalServerExceptions.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WebApp.Tools;

public class ExceptionFilter : IExceptionFilter
{
    private const HttpStatusCode DefaultHttpStatusCode = HttpStatusCode.InternalServerError;
    private const string DefaultHttpStatusMessage = "Ошибка при обработке запроса";
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var code = context.Exception switch
        {
            VibeNoteException ue => ue.Code,
            _ => DefaultHttpStatusCode
        };
            


        context.ExceptionHandled = true;
        var message = context.Exception switch
        {
            VibeNoteException ue => ue switch
            {
                InternalServerException se => DefaultHttpStatusMessage,
                not null => ue.Message,
            },
            _ => DefaultHttpStatusMessage
        };
        
        var result = new JsonResult(message)
        {
            StatusCode = (int)code
        };

        _logger.LogError($"Error while handling request: \n {context.Exception}");
        
        
        context.ExceptionHandled = false;
        context.Result = result;
    }
}