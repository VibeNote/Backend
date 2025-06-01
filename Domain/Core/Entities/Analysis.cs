using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities;

[Table("AnalysisTable")]
[PrimaryKey(nameof(Id))]
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
}