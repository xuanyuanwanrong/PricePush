using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PricePush.Data;
using Volo.Abp.DependencyInjection;

namespace PricePush.EntityFrameworkCore
{
    public class EntityFrameworkCorePricePushDbSchemaMigrator
        : IPricePushDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCorePricePushDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the PricePushDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<PricePushDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
