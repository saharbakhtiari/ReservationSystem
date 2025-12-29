using Application.Books.Commands.CreateBook;
using Application.Books.Commands.DeleteBook;
using Application.Books.Commands.UpdateBook;
using Application.Books.Queries.GetBookById;
using Application.Books.Queries.GetFilteredBooks;
using Application_Backend.Common;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BookController : ApiController
    {


        [HttpPost("search")]
        public async Task<IActionResult> GetFiltered([FromBody] GetFilteredBooksQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("adminsearch")]
        public async Task<IActionResult> GetFilteredAdmin([FromBody] GetFilteredBooksQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var News = await Mediator.SendWithUow(new GetBookByIdQuery() { Id = id });
            return Ok(News);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBookCommand dto)
        {
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] UpdateBookCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteById(DeleteBookCommand dto)
        {
            var News = await Mediator.SendWithUow(dto);
            return Ok(News);
        }


        //[HttpPost("uploadfile")]
        //public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        //{
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        file.CopyTo(memoryStream);
        //        StoreNewsFileCommand command = new()
        //        {
        //            DataFiles = memoryStream.ToArray(),
        //            Name = file.FileName,
        //            FileType = file.ContentType
        //        };
        //        var id = await Mediator.SendWithUow(command);
        //        return Ok(id);
        //    }
        //}

        //[HttpPost("deletefile")]
        //public async Task<IActionResult> DeleteFile(DeleteNewsFileCommand dto)
        //{
        //    await Mediator.SendWithUow(dto);
        //    return Ok(true);
        //}
    }
}
