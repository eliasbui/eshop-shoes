namespace Eshop.Infrastructure.EfCore;

public static class ConstSqlServer
{
    public const string UuidGenerator = "newid()";
    public const string UuidAlgorithm = "newid()";
    public const string DateAlgorithm = "getdate()";
}