using Magicodes.ExporterAndImporter.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Linq;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Zhaoxi.Forum.Application.Contracts.Category;
using Zhaoxi.Forum.Application.Contracts.Topic;
using Zhaoxi.Forum.Application.Contracts.User;

namespace Zhaoxi.Forum.DbMigrator;
public class DefaultDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ILogger<DefaultDataSeedContributor> _logger;
    private readonly IImporter _importer;
    private readonly ICategoryAppService _categoryAppService;
    private readonly ITopicAppService _topicAppService;
    private readonly IUserAppService _userAppService;

    public DefaultDataSeedContributor(
        IImporter importer
        ,ICategoryAppService categoryAppService
        ,ILogger<DefaultDataSeedContributor> logger
        ,ITopicAppService topicAppService
        ,IUserAppService userAppService
        )
    {
        _logger = logger;
        _importer = importer;
        _categoryAppService = categoryAppService;
        _topicAppService = topicAppService;
        _userAppService = userAppService;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        _logger.LogInformation("initial forum send data ...");
        try
        {
            if (!await _categoryAppService.AnyAsync())
            {
                await ImportCategoryAsync();
            }

            if (!await _topicAppService.AnyAsync())
            {
                await ImportTopicAsync();
            }
            if (!await _userAppService.AnyAsync())
            {
                await ImportUserAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message + ex.StackTrace);
        }
    }
    
    private async Task ImportCategoryAsync()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "category.xlsx");
        var import = _importer.Import<CategoryImportDto>(filePath);
        if (import.IsCompleted && import.Result != null && !import.Result.HasError)
        {
            var importDtos = import.Result.Data;
            await _categoryAppService.ImportAsync(importDtos);
        }
        else
        {
            //错误日志信息
            var errMsgs = "";
            foreach(var row in import.Result.RowErrors )
            {
                foreach(var item in row.FieldErrors )
                {
                    errMsgs += item.Key + ":" + item.Value;
                }
            }
            var test =import?.Result?.RowErrors[0].FieldErrors?.FirstOrDefault().Key+import?.Result?.RowErrors[0].FieldErrors?.FirstOrDefault().Value;
            _logger.LogError($"导入报错了,{errMsgs}");
        }
    }
    public async Task ImportUserAsync()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "user.xlsx");
        var import = _importer.Import<UserImportDto>(filePath);
        if (import.IsCompleted && import.Result != null)
        {
            var importDtos = import.Result.Data;
            await _userAppService.ImportAsync(importDtos);
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