using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CustomerService.Infrastructure.Data.CompiledModels;

partial class MainDbContextModel
{
    partial void Initialize()
    {
        var creditCard = CreditCardEntityType.Create(this);
        var customer = CustomerEntityType.Create(this);

        CreditCardEntityType.CreateForeignKey1(creditCard, customer);

        CreditCardEntityType.CreateAnnotations(creditCard);
        CustomerEntityType.CreateAnnotations(customer);

        AddAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");
        AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        AddAnnotation("ProductVersion", "8.0.2");
        AddAnnotation("Relational:MaxIdentifierLength", 63);
    }
}