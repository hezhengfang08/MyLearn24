
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MySelf.PMS.Server.IService;

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
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(fileService.GetUpgradeFiles());
        }

        // http://localhost:5000/api/file/download/modules/dll_name
        // http://localhost:5273/api/File/download/none/Zhaoxi.PMS.Client.BLL.dll
        [HttpGet("download/{p}/{file}")]
        public ActionResult Download([FromRoute(Name = "p")] string path, [FromRoute] string file)
        {
            // 服务器中存放更新文件的根目录
            var root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string filePath = Path.Combine(root, "UpgradeFiles");
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
    }
    internal class ResFileStream: FileStream
    {
        public ResFileStream(string path, FileMode mode,FileAccess fileAccess):base(path,mode,fileAccess)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer">缓冲区</param>
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
