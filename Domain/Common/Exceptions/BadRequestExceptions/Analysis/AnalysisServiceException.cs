using Common.Exceptions.BadRequestExceptions.Abstractions;

namespace Common.Exceptions.BadRequestExceptions.Analysis;

public class AnalysisServiceException: BadRequestException
{
    protected AnalysisServiceException(string message) : base(message)
    {
    }

    public static AnalysisServiceException NoTagsReturned()
        => throw new AnalysisServiceException("Наша нейросеть не смогла распознать эмоции по тексту");
    
    public static AnalysisServiceException NoResultReturned()
        => throw new AnalysisServiceException("Наша нейросеть не смогла дать совет");
}