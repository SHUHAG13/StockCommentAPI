using Microsoft.AspNetCore.Mvc;
using StockComment.Helpers;
using StockComment.Interfaces;
using StockComment.Mappers;
using StockComment.Models.Dtos;
namespace StockComment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockInterface _repo;
        public StockController(IStockInterface repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stocks = await _repo.GetAllAsync(query);
            var stockall=stocks.Select(s=>s.ToStockDto());
            return Ok(stocks);
        }

        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _repo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = stockDto.ToStockFromCreateDto();
            await _repo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id },stockModel.ToStockDto());
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _repo.UpdateAsync(id, updateDto);
            if (stockModel == null)
            {
                return NotFound();
            }
            return Ok(stockModel.ToStockDto());
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _repo.Delete(id);
            if(stockModel==null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
