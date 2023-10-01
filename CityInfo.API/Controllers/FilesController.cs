using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CompanyInfo.API.Controllers
{

    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        FileExtensionContentTypeProvider fileExtensionContentTypeProvider;
        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            this.fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
        }
        [HttpGet("{fileID}")]

        public ActionResult GetFile(string fileID)
        {
            string filePath = "webapiBanner.rar";
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            if (!fileExtensionContentTypeProvider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);

            

            return File(bytes, contentType, Path.GetFileName(filePath));


        }
    }
}
