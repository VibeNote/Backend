using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities;

[Table("EntryTable")]
[PrimaryKey(nameof(Id))]
public class Entry: IEntity<Guid>
{
    public Entry(Guid id, Guid userId, string content, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        UserId = userId;
        Content = content;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
    protected Entry() : this(Guid.Empty, Guid.Empty, string.Empty, DateTime.Now, DateTime.Now)
    {
    }
    [Key, Column("Id")]
    public Guid Id { get; }
    [Column("UserId")]
    public Guid UserId { get; protected set; }
    [Column("Content")]
    public string Content { get; set; }
    [Column("CreatedAt")]
    public DateTime CreatedAt { get; protected set; }
    [Column("UpdatedAt")]
    public DateTime UpdatedAt { get; set; }

    public virtual Analysis? Analysis { get; protected set; } = null;
}