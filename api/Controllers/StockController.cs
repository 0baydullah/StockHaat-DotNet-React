using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock/")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepo _stockRepo;

        public StockController(IStockRepo stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var stocks = await _stockRepo.GetAllAsync(query);
            var stockDtos = stocks.Select(stock => stock.ToDto()).ToList();

            if (stockDtos == null || !stockDtos.Any())
            {
                return NotFound("No stocks found.");
            }

            return Ok(stockDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound($"Stock with ID {id} not found.");
            }

            return Ok(stock.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] StockCreateDto stockDto)
        {
            var stock = stockDto.ToStockFromRequest();
            await _stockRepo.CreateAsync(stock);

            return CreatedAtAction(nameof(GetById), new { Id = stock.Id }, stock.ToDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] StockUpdateDto stockUpdateDto)
        {
            var stock = await _stockRepo.UpdateAsync(id, stockUpdateDto);

            if (stock == null)
            {
                return NotFound($"Stock with ID {id} not found that you are trying to update.");
            }

            return Ok(stock.ToDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepo.DeleteAsync(id);

            if (stock == null)
            {
                return NotFound($"Stock with id {id} isn't found that u are trying to delete");
            }

            return NoContent();
        }
    }
}
