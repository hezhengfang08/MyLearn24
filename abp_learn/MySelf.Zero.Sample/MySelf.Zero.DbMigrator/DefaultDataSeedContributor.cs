using Magicodes.ExporterAndImporter.Core;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using MySelf.Zero.Application.Contracts.Category;
using MySelf.Zero.Application.Contracts.Topic;
using MySelf.Zero.Application.Contracts.User;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace MySelf.Zero.DbMigrator
{
    public class DefaultDataSeedContributor: IDataSeedContributor, ITransientDependency
    {
        private readonly ILogger<DefaultDataSeedContributor> logger;
        private readonly IImporter importer;
        private readonly ICategoryAppService categoryAppService;
        private readonly ITopicAppService topicAppService;
        private readonly IUserAppService userAppService;

        public DefaultDataSeedContributor(IImporter importer,
            ICategoryAppService categoryAppService,
            ITopicAppService topicAppService,
            IUserAppService userAppService)
        {
            this.logger = NullLogger<DefaultDataSeedContributor>.Instance;
            this.importer = importer;
            this.categoryAppService = categoryAppService;
            this.topicAppService = topicAppService;
            this.userAppService = userAppService;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            logger.LogInformation("initial forum send data ...");

            try
            {
                if (!await categoryAppService.AnyAsync())
                {
                    await ImportCategoryAsync();
                }

                if (!await topicAppService.AnyAsync())
                {
                    await ImportTopicAsync();
                }

                if (!await userAppService.AnyAsync())
                {
                    await ImportUserAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + ex.StackTrace);
            }
        }

        private async Task ImportCategoryAsync()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "category.xlsx");
            var import = importer.Import<CategoryImportDto>(filePath);
            if (import.IsCompleted && import.Result != null)
            {
                var importDtos = import.Result.Data;
                await categoryAppService.ImportAsync(importDtos);
            }
        }

        private async Task ImportTopicAsync()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "topic.xlsx");
            var import = importer.Import<TopicImportDto>(filePath);
            if (import.IsCompleted && import.Result != null)
            {
                var importDtos = import.Result.Data;
                await topicAppService.ImportAsync(importDtos);
            }
        }

        public async Task ImportUserAsync()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "user.xlsx");
            var import = importer.Import<UserImportDto>(filePath);
            if (import.IsCompleted && import.Result != null)
            {
                var importDtos = import.Result.Data;
                await userAppService.ImportAsync(importDtos);
            }
        }
    }
}
