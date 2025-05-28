using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities;

[Table("TokenTable")]
[PrimaryKey(nameof(Id))]
public class Token: IEntity<Guid>
{
    public Token(Guid id, Guid userId, string value)
    {
        UserId = userId;
        Value = value;
        Id = id;
    }
    
    
    protected Token(): this(Guid.Empty, Guid.Empty, string.Empty){}
    [Key, Column("Id")]
    public Guid Id { get; protected set; }
    [Column("UserId")]
    public Guid UserId { get; protected set; }
    [Key, Column("Value")]
    public string Value { get; protected set; }
}