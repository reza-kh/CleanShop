using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common;


public static class Guard
{
    public static void AgainstNull<T>(T? value, string name)
    {
        if (value is null) throw new DomainException($"{name} must not be null.");
    }

    public static void AgainstNullOrEmpty(string? value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException($"{name} must not be empty.");
    }

    public static void AgainstOutOfRange(int value, int minInclusive, string name)
    {
        if (value < minInclusive)
            throw new DomainException($"{name} must be >= {minInclusive}.");
    }

    public static void AgainstNegative(decimal value, string name)
    {
        if (value < 0) throw new DomainException($"{name} must be >= 0.");
    }
}
