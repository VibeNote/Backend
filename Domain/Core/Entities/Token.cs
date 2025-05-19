using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Abstractions;

namespace Core.Entities;

[Table("TokenTable")]
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
    [Column("Value")]
    public string Value { get; protected set; }
}