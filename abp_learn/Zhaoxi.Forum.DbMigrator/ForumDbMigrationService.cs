using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Volo.Abp.DependencyInjection;

namespace Zhaoxi.Forum.DbMigrator;

public class ForumDbMigrationService : ITransientDependency
{
    public ILogger<ForumDbMigrationService> Logger { get; set; }

    public ForumDbMigrationService()
    {
        Logger = NullLogger<ForumDbMigrationService>.Instance;
    }

    public void Migrate()
    {
        // 初始化迁移脚本
        var initialMigrationAdded = AddInitialMigrationIfNotExist();
        if (initialMigrationAdded)
        {
            return;
        }
        else
        {
            // 更新迁移脚本到数据库
            UpdateInitialMigration();
        }
    }

    private bool AddInitialMigrationIfNotExist()
    {
        try
        {
            if (!MigrationsFolderExists())
            {
                AddInitialMigration();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Logger.LogWarning("Couldn't determinate if any migrations exist : " + e.Message);
            return false;
        }
    }

    private bool MigrationsFolderExists()
    {
        var dbMigrationsProjectFolder = GetDbMigratorProjectFolderPath();

        return Directory.Exists(Path.Combine(dbMigrationsProjectFolder, "Migrations"));
    }

    private void UpdateInitialMigration()
    {
        Logger.LogInformation("Update initial migration...");

        string argumentPrefix;
        string fileName;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            argumentPrefix = "-c";
            fileName = "/bin/bash";
        }
        else
        {
            argumentPrefix = "/C";
            fileName = "cmd.exe";
        }


        var procStartInfo = new ProcessStartInfo(fileName,
            $"{argumentPrefix} dotnet ef database update"
        );
        procStartInfo.WorkingDirectory = GetDbMigratorProjectFolderPath();

        try
        {
            Process.Start(procStartInfo);
        }
        catch (Exception ex)
        {
            Logger.LogError("Update initial migration...", ex.Message);
            throw new Exception("Couldn't run dotnet ef migrations...");
        }
    }

    private void AddInitialMigration()
    {
        Logger.LogInformation("Creating initial migration...");

        string argumentPrefix;
        string fileName;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            argumentPrefix = "-c";
            fileName = "/bin/bash";
        }
        else
        {
            argumentPrefix = "/C";
            fileName = "cmd.exe";
        }


        var procStartInfo = new ProcessStartInfo(fileName,
            $"{argumentPrefix} dotnet ef migrations add InitialCreate"
        );
        procStartInfo.WorkingDirectory = GetDbMigratorProjectFolderPath();

        try
        {
            Process.Start(procStartInfo);
        }
        catch (Exception ex)
        {
            Logger.LogError("Creating initial migration...", ex.Message);
            throw new Exception("Couldn't run dotnet ef migrations...");
        }
    }

    private string GetDbMigratorProjectFolderPath()
    {
        var slnDirectoryPath = GetSolutionDirectoryPath();

        if (slnDirectoryPath == null)
        {
            throw new Exception("Solution folder not found!");
        }

        var srcDirectoryPath = Path.Combine(slnDirectoryPath);
        //var srcDirectoryPath = Path.Combine(slnDirectoryPath, "src");

        return Directory.GetDirectories(srcDirectoryPath)
                        .FirstOrDefault(d => d.EndsWith(".DbMigrator"));
    }

    private string GetSolutionDirectoryPath()
    {
        var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (Directory.GetParent(currentDirectory.FullName) != null)
        {
            currentDirectory = Directory.GetParent(currentDirectory.FullName);

            if (Directory.GetFiles(currentDirectory.FullName).FirstOrDefault(f => f.EndsWith(".sln")) != null)
            {
                return currentDirectory.FullName;
            }
        }

        return null;
    }
}