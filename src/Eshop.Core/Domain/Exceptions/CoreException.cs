using System;

namespace Eshop.Core.Domain.Exceptions;

public class CoreException(string message) : Exception(message)
{
    public static CoreException Exception(string message)
    {
        return new CoreException(message);
    }

    public static CoreException NullArgument(string arg)
    {
        return new CoreException($"{arg} cannot be null");
    }

    public static CoreException InvalidArgument(string arg)
    {
        return new CoreException($"{arg} is invalid");
    }

    public static CoreException NotFound(string arg)
    {
        return new CoreException($"{arg} was not found");
    }
}