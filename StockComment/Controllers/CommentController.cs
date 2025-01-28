using Microsoft.AspNetCore.Mvc;
using StockComment.Interfaces;
using StockComment.Mappers;
using StockComment.Models.Dtos;
namespace StockComment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentInterface _repository;

        public CommentController(ICommentInterface repository)
        {
            _repository = repository;   
        }

        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var comments = await _repository.GetAllAsync();
            return Ok(comments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult>GetById([FromRoute]int id)
        {
            var comment = await _repository.GetByIdAsync(id);
            return Ok(comment);
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto commentDto)
        {
            var commentModel = commentDto.ToCommentFromCreateDto();
            await _repository.CreateCommentAsync(commentModel);
            return CreatedAtAction(nameof(GetById),new { id=commentModel.Id},commentModel.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]UpdateCommentRequestDto updateDto)
        {
            var commentModel = await _repository.UpdateCommentAsync(id, updateDto);
            if(commentModel == null)
            {
                return NotFound();
            }
            return Ok(commentModel.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commentModel = await _repository.DeleteAsync(id);
            if(commentModel==null)
            {
                return NotFound();
            }
            return NoContent();
        }



    }
}
