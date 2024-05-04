using System.Threading.Tasks;

namespace PricePush.Data
{
    public interface IPricePushDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
