using Magicodes.ExporterAndImporter.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Zhaoxi.Forum.Application.Contracts;

namespace Zhaoxi.Forum.DbMigrator;

public class DefaultDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ILogger<DefaultDataSeedContributor> _logger;
    private readonly IImporter _importer;
    private readonly ICategoryAppService _categoryAppService;
    private readonly ITopicAppService _topicAppService;

    public DefaultDataSeedContributor(IImporter importer,
        ICategoryAppService categoryAppService,
        ITopicAppService topicAppService)
    {
        _logger = NullLogger<DefaultDataSeedContributor>.Instance;
        _importer = importer;
        _categoryAppService = categoryAppService;
        _topicAppService = topicAppService;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        _logger.LogInformation("initial forum send data ...");

        if (!await _categoryAppService.AnyAsync())
        {
            await ImportCategoryAsync();
        }

        if (!await _topicAppService.AnyAsync())
        {
            await ImportTopicAsync();
        }
    }

    private async Task ImportCategoryAsync()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "category.xlsx");
        var import = _importer.Import<CategoryImportDto>(filePath);
        if (import.IsCompleted && import.Result != null)
        {
            var importDtos = import.Result.Data;
            await _categoryAppService.ImportAsync(importDtos);
        }
    }

    private async Task ImportTopicAsync()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "topic.xlsx");
        var import = _importer.Import<TopicImportDto>(filePath);
        if (import.IsCompleted && import.Result != null)
        {
            var importDtos = import.Result.Data;
            await _topicAppService.ImportAsync(importDtos);
        }
    }
}