using System;
using System.Diagnostics;

namespace Eshop.Core.Domain.Helpers;

public class GuidHelper
{
    [DebuggerStepThrough]
    public static Guid NewGuid(string? guid = null)
    {
        return string.IsNullOrWhiteSpace(guid) ? Guid.NewGuid() : new Guid(guid);
    }
}