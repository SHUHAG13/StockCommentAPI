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
        private readonly IStockInterface _stockRepo;

        public CommentController(ICommentInterface repository,IStockInterface stockRepo)
        {
            _repository = repository;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var comments = await _repository.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult>GetById([FromRoute]int id)
        {
            var comment = await _repository.GetByIdAsync(id);
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute]int stockId,[FromBody] CreateCommentRequestDto commentDto)
        {
            if(!await _stockRepo.StockExist(stockId))
            {
                return BadRequest("stock does not exist");
            }


            var commentModel = commentDto.ToCommentFromCreateDto(stockId);
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
