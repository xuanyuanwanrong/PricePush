using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace PricePush.Data
{
    /* This is used if database provider does't define
     * IPricePushDbSchemaMigrator implementation.
     */
    public class NullPricePushDbSchemaMigrator : IPricePushDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}