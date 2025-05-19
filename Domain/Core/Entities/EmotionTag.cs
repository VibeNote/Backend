using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Common.Exceptions.InternalServerExceptions.Domain.Operation;
using Core.Abstractions;

namespace Core.Entities;

[Table("EmotionTagTable")]
public class EmotionTag: IEntity<Guid>
{
    
    private readonly List<TriggerWord> _triggerWords;
    public EmotionTag(Guid id, Guid analysisId, Guid tagId, int value)
    {
        Id = id;
        AnalysisId = analysisId;
        TagId = tagId;
        Value = value;
    }

    protected EmotionTag() : this(Guid.Empty, Guid.Empty, Guid.Empty, 0)
    {
    }
    [Key, Column("Id")]
    public Guid Id { get; protected set; }
    [Column("AnalysisId")]
    public Guid AnalysisId { get; protected set; }
    [Column("TagId")]
    public Guid TagId { get; protected set; }
    [Column("Value")]
    public int Value { get; set; }
    public virtual IReadOnlyCollection<TriggerWord> TriggerWords => _triggerWords;
    
    [ForeignKey("TagId")]
    public virtual Tag Tag { get; protected set; } = null!;

        
    public void AddEmotionTag(TriggerWord triggerWord)
    {
        if (_triggerWords.Any(tw => tw.Id == triggerWord.Id))
        {
            throw EmotionTagInvalidDomainOperationException.EmotionTagCannotAddTriggerWord(Id, triggerWord.Id);
        }
        
        _triggerWords.Add(triggerWord);
    }
    
}