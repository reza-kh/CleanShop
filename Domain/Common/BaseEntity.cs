using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; private set; } = DateTime.UtcNow;
    public string CreatorUserId { get; private set; }

    public DateTime? LastModifiedDate { get; private set; }
    public string? LastModifiedUserId { get; private set; }

    public bool IsDeleted { get; private set; } = false;

    // --- Constructors ---
    protected BaseEntity()
    {
    }

    protected BaseEntity(string creatorUserId)
    {
        Id = Guid.NewGuid();
        CreatorUserId = creatorUserId;
        CreationDate = DateTime.UtcNow;
        IsDeleted = false;
    }

    protected BaseEntity(Guid id, string creatorUserId)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        CreatorUserId = creatorUserId;
        CreationDate = DateTime.UtcNow;
        IsDeleted = false;
    }

    public void SetLastModified(string userId)
    {
        LastModifiedDate = DateTime.UtcNow;
        LastModifiedUserId = userId;
    }


    public void SoftDelete() => IsDeleted = true;
    public void Restore() => IsDeleted = false;
}
