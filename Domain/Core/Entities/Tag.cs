using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Abstractions;

namespace Core.Entities;

[Table("TagTable")]
public class Tag: IEntity<Guid>
{
    public Tag(Guid id, string value)
    {
        Id = id;
        Value = value;
    }

    protected Tag() : this(Guid.Empty, string.Empty) {}
    [Key, Column("Id")]
    public Guid Id { get; }
    [Column("Value")]
    public string Value { get; set; }
}