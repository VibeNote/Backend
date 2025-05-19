using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Common.Exceptions.InternalServerExceptions.Domain.Operation;
using Core.Abstractions;

namespace Core.Entities;

[Table("AnalysisTable")]
public class Analysis: IEntity<Guid>
{
    private readonly List<EmotionTag> _emotionTags;
    
    public Analysis(Guid id, Guid entryId, string result, DateTime createdAt)
    {
        Id = id;
        EntryId = entryId;
        Result = result;
        CreatedAt = createdAt;
        _emotionTags = new List<EmotionTag>();
    }

    protected Analysis() : this(Guid.Empty, Guid.Empty, string.Empty, DateTime.Now) {}
    
    [Key, Column("Id")]
    public Guid Id { get; protected set; }
    [Column("EntryId")]
    public Guid EntryId { get; protected set; }
    [Column("Result")]
    public string Result { get; protected set; }
    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; }
    
    public virtual IReadOnlyCollection<EmotionTag> EmotionTags => _emotionTags;

    public void AddEmotionTag(EmotionTag emotionTag)
    {
        if (_emotionTags.Any(et => et.Id == emotionTag.Id))
        {
            throw AnalysisInvalidDomainOperationException.AnalysisCannotAddEmotionTag(Id, emotionTag.Id);
        }
        
        _emotionTags.Add(emotionTag);
    }
}