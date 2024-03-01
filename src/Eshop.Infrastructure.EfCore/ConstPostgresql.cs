namespace Eshop.Infrastructure.EfCore;

public static class ConstPostgresql
{
    public const string UuidGenerator = "uuid-ossp";
    public const string UuidAlgorithm = "uuid_generate_v4()";
    public const string DateAlgorithm = "now()";
}