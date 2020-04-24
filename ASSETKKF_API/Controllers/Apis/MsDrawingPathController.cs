using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Apis.Mcis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace ASSETKKF_API.Controllers.Apis
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class MsDrawingPathController : Base
    {
        private IHostingEnvironment _hostingEnvironment;

        public MsDrawingPathController(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [HttpPost("GetData")]
        public async Task<dynamic> GetData([FromBody] dynamic data)
        {
            var res = new MsDrawingPathGetDataApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("Search")]
        public async Task<dynamic> Search([FromBody] dynamic data)
        {
            var res = new MsDrawingPathSearchApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

         

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        [HttpGet("GetFile")] //download
        public async Task<dynamic> GetFile()
        {

            // var res = new MsDrawingPathSearchApi();
            //return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

            String fileName = "B6407150109201_3.PDF";
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();

            //  currentDirectory = currentDirectory + "\\src\\assets";
            currentDirectory = "//191.20.2.9/mcis/product/";

           
            //var file = Path.Combine(Path.Combine(currentDirectory, "attachments"), fileName);


            var file = Path.Combine(currentDirectory, fileName);

          return new FileStream(file, FileMode.Open, FileAccess.Read);
            /*
            
    var result = await nodeServices.InvokeAsync<byte[]>("./pdf");

    HttpContext.Response.ContentType = "application/pdf";

    string filename = @"report.pdf";
    HttpContext.Response.Headers.Add("x-filename", filename);
    HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "x-filename");
    HttpContext.Response.Body.Write(result, 0, result.Length);
    return new ContentResult();

            */
             


        }


        [HttpGet("DownloadFile")] //download
        public async Task<dynamic> DownloadFile()
        {
            
            // var res = new MsDrawingPathSearchApi();
            //return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

            String fileName = "B6407150109201_3.PDF";
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
      
            //  currentDirectory = currentDirectory + "\\src\\assets";
            currentDirectory = "//191.20.2.9/mcis/product/";


            //var file = Path.Combine(Path.Combine(currentDirectory, "attachments"), fileName);


            var file = Path.Combine( currentDirectory , fileName);
            //return new FileStream(file, FileMode.Open, FileAccess.Read);

            var memory = new MemoryStream();
            using (var stream = new FileStream(file, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(file), fileName);
                       
        }
             


         
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            if (file.Length > 0)
            {
                var filePath = Path.Combine(uploads, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return Ok();
        }





    }
}