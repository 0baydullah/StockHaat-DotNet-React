using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepo
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> UpdateAsync(int id, CommentUpdateDto commentDto);
        Task<Comment?> DeleteAsync(int id);
        Task<List<Comment>> GetCommentsByStockIdAsync(int stockId);
    }
}
