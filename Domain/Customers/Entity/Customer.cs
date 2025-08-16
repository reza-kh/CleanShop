using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entity;

public sealed class Customer : BaseEntity
{
    public string FullName { get; }
    public string Email { get; }

    public Customer() : base()
    {
    }
    public Customer(string fullName, string email, string creatorUserId) : base(creatorUserId)
    {
        Guard.AgainstNullOrEmpty(fullName, nameof(fullName));
        Guard.AgainstNullOrEmpty(email, nameof(email));

        FullName = fullName;
        Email = email;
    }
}