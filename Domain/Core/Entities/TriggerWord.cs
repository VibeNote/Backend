using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities;

[Table("TriggerWordTable")]
[PrimaryKey(nameof(Id))]
public class TriggerWord: IEntity<Guid>
{
    public TriggerWord(Guid id, Guid tagId, Guid userId, Guid analysisId, string word)
    {
        Id = id;
        TagId = tagId;
        UserId = userId;
        AnalysisId = analysisId;
        Word = word;
    }
    
    protected TriggerWord(): this(Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, string.Empty) {}
    [Key, Column("Id")]
    public Guid Id { get; }
    [Column("TagId")]
    public Guid TagId { get; protected set; }
    [Column("UserId")]
    public Guid UserId { get; protected set; }
    [Column("AnalysisId")]
    public Guid AnalysisId { get; protected set; }
    [Column("Word")]
    public string Word { get; protected set; }
}