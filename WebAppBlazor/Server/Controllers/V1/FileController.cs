using Application.SeoFiles.Commands.DeleteSeoFile;
using Application.SeoFiles.Commands.StoreSeoFile;
using Application_Backend.Common;
using Application_Backend.SeoFiles.Queries.GetFilteredSeoFiles;
using Application_Backend.SeoFiles.Queries.GetSeoFileById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class FileController : ApiController
    {


        [HttpPost("search")]
        public async Task<IActionResult> GetFiltered([FromBody] GetFilteredSeoFilesQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                StoreSeoFileCommand command = new()
                {
                    DataFiles = memoryStream.ToArray(),
                    Name = file.FileName,
                    FileType = file.ContentType
                };
                var dto = await Mediator.SendWithUow(command);
                return Ok(dto);
            }
        }

        [HttpDelete("deletefile/{fileGuid}")]
        public async Task<IActionResult> DeleteFile(Guid fileGuId)
        {
            await Mediator.SendWithUow(new DeleteSeoFileCommand { FileGuid = fileGuId });
            return Ok(true);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var Footer = await Mediator.SendWithUow(new GetSeoFileByIdQuery() { Id = id });
            return Ok(Footer);
        }
    }
}
