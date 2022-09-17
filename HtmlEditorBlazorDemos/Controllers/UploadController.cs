using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace HtmlEditorBlazorDemos
{
    [DisableRequestSizeLimit]
    public partial class UploadController : Controller
    {
        private readonly IWebHostEnvironment environment;

        public UploadController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        [HttpPost("upload/image")]
        public IActionResult Image(IFormFile file)
        {
            try
            {
                // Used for demo purposes only
                DeleteOldFiles();

                var fileName = $"upload-{DateTime.Today.ToString("yyyy-MM-dd")}-{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                using (var stream = new FileStream(Path.Combine(environment.WebRootPath, fileName), FileMode.Create))
                {
                    // Save the file
                    file.CopyTo(stream);

                    // Return the URL of the file
                    var url = Url.Content($"~/{fileName}");

                    return Ok(new { Url = url });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        void DeleteOldFiles()
        {
            foreach (var file in Directory.GetFiles(environment.WebRootPath))
            {
                var fileName = Path.GetFileName(file);

                if (fileName.StartsWith("upload-") && !fileName.StartsWith($"upload-{DateTime.Today.ToString("yyyy-MM-dd")}"))
                {
                    try
                    {
                        System.IO.File.Delete(file);
                    }
                    catch
                    {

                    }
                }
            }
        }


    }
}