using System.Diagnostics;

namespace Eshop.Core.Domain.Helpers;

public class DateTimeHelper
{
    [DebuggerStepThrough]
    public static DateTime NewDateTime()
    {
        return DateTimeOffset.Now.UtcDateTime;
    }
}