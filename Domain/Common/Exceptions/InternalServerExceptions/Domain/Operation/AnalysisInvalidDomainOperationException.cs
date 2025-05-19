using System;
using Common.Exceptions.InternalServerExceptions.Abstractions;

namespace Common.Exceptions.InternalServerExceptions.Domain.Operation;

public class AnalysisInvalidDomainOperationException : InvalidDomainOperationException
{
    protected AnalysisInvalidDomainOperationException(string message): base(message)
    {
    }

    public static AnalysisInvalidDomainOperationException AnalysisCannotAddEmotionTag(
        Guid analysisId,
        Guid emotionTagId)
        => new AnalysisInvalidDomainOperationException(
            $"Analysis with id {analysisId} already has emotion tag with id {emotionTagId}"
        );
}