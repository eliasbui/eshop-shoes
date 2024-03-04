using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CustomerService.Infrastructure.Data.CompiledModels;

[DbContext(typeof(MainDbContext))]
partial class MainDbContextModel : RuntimeModel
{
    private static MainDbContextModel? _instance;

    public static IModel Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = new MainDbContextModel();
            _instance.Initialize();
            _instance.Customize();

            return _instance;
        }
    }

    partial void Initialize();

    partial void Customize();
}