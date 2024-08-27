
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MySelf.PMS.Server.IService;
using MySelf.PMS.Server.Entities;
using MySelf.PMS.Server.Models;
using MySelf.PMS.Server.Service;
using Microsoft.AspNetCore.Authorization;

namespace MySelf.PMS.Server.Start.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        IFileService fileService;
        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }
        [HttpGet("list/{key}")]
        public ActionResult Get([FromRoute] string key)
        {
            Result<UpgradeFileEntity[]> result = new Result<UpgradeFileEntity[]>();
            try
            {
                key = key == "none" ? "" : key;
                var fs = fileService.GetUpgradeFiles(key);
                result.Data = fs.ToArray();
            }
            catch (Exception ex)
            {
                result.State = StateEnum.Error;
                result.Message = ex.Message;
            }
            return Ok(result);
        }

        // http://localhost:5000/api/file/download/modules/dll_name
        // http://localhost:5273/api/File/download/none/Zhaoxi.PMS.Client.BLL.dll
        [HttpGet("download/{p}/{file}")]
        public ActionResult Download([FromRoute(Name = "p")] string path, [FromRoute] string file)
        {
            // 服务器中存放更新文件的根目录
            var root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string filePath = Path.Combine(root, "WebFiles", "UpgradeFiles");
            // 如果有子目录，则拼进来
            if (path != "none")
                filePath = Path.Combine(filePath, path);
            // 最终文件路径
            filePath = Path.Combine(filePath, file);

            ResFileStream fs = new ResFileStream(filePath, FileMode.Open, FileAccess.Read);
            var type = new MediaTypeHeaderValue("application/oct-stream").MediaType;
            // 最终返回文件对象
            return File(fs, type, fileDownloadName: file);
        }
        // http://localhost:5273/api/File/img/002m.jpg
        [HttpGet("img/{img}")]
        public IActionResult GetImage([FromRoute(Name = "img")] string imgPath)
        {
            var root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string rootPath = Path.Combine(root, @"WebFiles\UserAvatars");
            //获取图片的返回类型
            var contentTypDict = new Dictionary<string, string> {
                {"jpg","image/jpeg"},
                {"jpeg","image/jpeg"},
                {"jpe","image/jpeg"},
                {"png","image/png"},
                {"gif","image/gif"},
                {"ico","image/x-ico"},
                {"tif","image/tiff"},
                {"tiff","image/tiff"},
                {"fax","image/fax"},
                {"wbmp","image/nd.wap.wbmp"},
                {"rp","imagend.rn-realpix"}
            };
            var contentTypeStr = "image/jpeg";

            var imgTypeSplit = imgPath.Split('.');
            var imgType = imgTypeSplit[imgTypeSplit.Length - 1].ToLower();
            //未知的图片类型
            if (contentTypDict.ContainsKey(imgType))
            {
                contentTypeStr = contentTypDict[imgType];
            }

            string filePath = Path.Combine(rootPath, imgPath);
            //图片不存在
            if (!new FileInfo(filePath).Exists)
            {
                return NotFound();
            }
            //返回原图
            using (var sw = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var bytes = new byte[sw.Length];
                sw.Read(bytes, 0, bytes.Length);
                sw.Close();
                return new FileContentResult(bytes, contentTypeStr);
            }
        }

        [HttpPost("upload")]
        [Authorize]
        public IActionResult Upload(
            [FromForm] IFormCollection formCollection,// 用来传递文件
            [FromHeader] string md5,//计算文件的MD5值    进行标记
            [FromHeader] string save_path)// 文件需要存放的子目录   .\    Modules
        {
            Result<long> result = new Result<long>();
            try
            {
                FormFileCollection filelist = (FormFileCollection)formCollection.Files;
                if (filelist.Count > 0)
                {
                    // 文件名称
                    string fileName = filelist[0].FileName;

                    var root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                    string targetPath = Path.Combine(root, "WebFiles", "UpgradeFiles");
                    if (!string.IsNullOrEmpty(save_path) && save_path != ".\\")
                        targetPath = Path.Combine(targetPath, save_path);

                    DirectoryInfo di = new DirectoryInfo(targetPath);
                    if (!di.Exists) di.Create();
                    using (FileStream fs = System.IO.File.Create(Path.Combine(targetPath, fileName)))
                    {
                        // 复制文件
                        filelist[0].CopyToAsync(fs).GetAwaiter().GetResult();
                        // 清空缓冲区数据
                        fs.Flush();
                    }
                    // 更新或新增到数据库
                    UpgradeFileEntity upgradeFile = new UpgradeFileEntity
                    {
                        FileName = fileName,
                        FileMd5 = md5,
                        FilePath = save_path,
                        UploadTime = DateTime.Now,
                        Length = filelist[0].Length
                    };

                    result.Data = fileService.AddOrUpdate(upgradeFile);
                }
            }
            catch (Exception ex)
            {
                result.State = StateEnum.Error;
                result.Message = ex.Message;
            }
            return Ok(result);
        }

        [HttpPost("delete")]
        [Authorize]
        public ActionResult DeleteFile([FromForm] string fileName)
        {
            Result<int> result = new Result<int>();
            try
            {
                var file = fileService.GetFileByName(fileName);
                result.Data = fileService.Delete(fileName);// 删除数据库记录，文件
                // 删除存放文件
                //file.FilePath;
                //fileName
                var root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                string targetPath = Path.Combine(root, "WebFiles", "UpgradeFiles");
                if (!string.IsNullOrEmpty(file.FilePath) && file.FilePath != ".\\")
                    targetPath = Path.Combine(targetPath, file.FilePath);

                string full_path = Path.Combine(targetPath, fileName);
                if (System.IO.File.Exists(full_path))
                    System.IO.File.Delete(full_path);
            }
            catch (Exception ex)
            {
                result.State = StateEnum.Error;
                result.Message = ex.Message;
            }
            return Ok(result);
        }
    }

    internal class ResFileStream : FileStream
    {
        public ResFileStream(string path, FileMode mode, FileAccess access) : base(path, mode, access) { }

        /// <param name="array"></param>
        /// <param name="offset">偏移量</param>
        /// <param name="count">最大字符串</param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            // 此处可以限制下载速度
            //count = 256;
            //Thread.Sleep(10);
            return base.Read(buffer, offset, count);
        }
    }
}
