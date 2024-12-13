using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MySelf.Zhihu.Infrastructure.Data;
using MySelf.Zhihu.UseCases.Common.Interfaces;

namespace MySelf.Zhihu.UseCases.FunctionalTests
{
    public abstract class TestBase
    {
        protected TestBase(IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<DbInitializer>().InitialCreate();

            Sender = serviceProvider.GetRequiredService<ISender>();

            CurrentUser = serviceProvider.GetRequiredService<IUser>();

            DbContext = serviceProvider.GetRequiredService<AppDbContext>();
        }

        public ISender Sender { get; private set; }

        public IUser CurrentUser { get; private set; }

        public AppDbContext DbContext { get; private set; }
    }
}
