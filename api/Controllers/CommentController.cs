using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment/")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IStockRepo _stockRepo;

        public CommentController(ICommentRepo commentRepo, IStockRepo stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var comments = await _commentRepo.GetAllAsync();
                var commentDtos = comments.Select(c => c.ToCommnetDto()).ToList();

                return Ok(commentDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving comments: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            
            if (comment == null)
            {
                return NotFound($"Comment with ID {id} not found.");
            }

            return Ok(comment.ToCommnetDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, [FromBody] CommentCreateDto commentDto)
        {
            if(!await _stockRepo.StockExists(stockId))
            {
                return BadRequest($"Stock with ID {stockId} not exist.");
            }

            var comment = commentDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(comment);

            return CreatedAtAction(nameof(GetById), new { Id = comment.Id }, comment.ToCommnetDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] CommentUpdateDto commentDto)
        {
            var updatedComment = await _commentRepo.UpdateAsync(id, commentDto);

            if (updatedComment == null)
            {
                return NotFound($"Comment with ID {id} not found.");
            }

            return Ok(updatedComment.ToCommnetDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepo.DeleteAsync(id);

            if(comment == null)
            {
                return NotFound($"Comment with id {id} not found");
            }

            return Ok($"Comment with id {id} deleted successfully");
        }
    }
}
