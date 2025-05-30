using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities;

[Table("UserTable")]
[PrimaryKey(nameof(Id))]
public class User: IEntity<Guid>
{
    public User(Guid id, string userName, string login, string passwordHash, DateTime createdAt)
    {
        Id = id;
        UserName = userName;
        Login = login;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
        IsAuthBlocked = false;
        BlockedTill = DateTime.Now;
    }

    [Key, Column("Id")]
    public Guid Id { get; protected set; }
    [Column("UserName")]
    public string UserName { get; set; }
    [Key, Column("Login")]
    public string Login { get; protected set; }
    [Column("PasswordHash")]
    public string PasswordHash { get; protected set; }
    [Column("CreatedAt")]
    public DateTime CreatedAt { get; protected set; }
    [Column("IsAuthBlocked")]
    public bool IsAuthBlocked { get; set; }
    [Column("BlockedTill")]
    public DateTime BlockedTill { get; set; }
}