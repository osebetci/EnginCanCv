using EnginCan.Core.Helpers.Files;
using EnginCan.Core.Middleware;
using EnginCan.Dto.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnginCan.Api.Controllers.Docs
{
    [Route("api/docs/images")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageProccess _imageProccess;
        private readonly IHostingEnvironment _customDirectory;

        /// <summary>
        /// Yapıcı method.
        /// </summary>
        /// <param name="customDirectory"></param>
        /// <param name="imageProccess"></param>
        public ImagesController(IHostingEnvironment customDirectory, IImageProccess imageProccess)
        {
            _imageProccess = imageProccess;
            _customDirectory = customDirectory;
        }

        /// <summary>
        /// Resimleri croplar ve görüntüler
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet, Route("{name}")]
        public IActionResult Get(string name)
        {
            string path = Path.Combine(_customDirectory.ContentImagePath(), name);
            if (!System.IO.File.Exists(path))
                return NotFound();

            new FileExtensionContentTypeProvider().TryGetContentType(path, out string contentType);

            return PhysicalFile(path, contentType);
        }

        /// <summary>
        /// Resimleri croplamaya ve görüntülenmesini sağlayan uçtur.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet, Route("{query}/{name}")]
        public IActionResult GetQuery(string query, string name)
        {
            string path = _imageProccess.QueryResult(query, name);
            if (path == null)
                return NotFound();

            new FileExtensionContentTypeProvider().TryGetContentType(path, out string contentType);

            return PhysicalFile(path, contentType);
        }

        /// <summary>
        /// Resimlerin yüklenmesini sağlayan uçtur.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost, Route("upload")]
        public async Task<IActionResult> Upload([FromBody] UploadImage post)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(post.Image))
                return BadRequest(ModelState);

            try
            {
                string type = post.Image.Split(',')[0].Replace("data:image/", "").Replace(";base64", "");
                string data = post.Image.Split(',')[1];
                string fileName = $"{DateTime.Now.Ticks}.{type}";
                string rootFolder = _customDirectory.ContentImagePath();

                var filePath = Path.Combine(rootFolder, fileName);
                using (var file = new StreamContent(new MemoryStream(Convert.FromBase64String(data))))
                using (var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                    await file.CopyToAsync(fileStream);

                return Ok(new { FileName = fileName });
            }
            catch
            {
                return Ok(new Response(false, "Birşeyler yanlış gitti."));
            }
        }


    }
}
