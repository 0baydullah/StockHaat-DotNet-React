using api.Dtos.Comment;
using api.Models;
using System.Runtime.CompilerServices;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommnetDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }

        public static Comment ToComment(this CommentDto commentDto)
        {
            return new Comment
            {
                Id = commentDto.Id,
                Title = commentDto.Title,
                Content = commentDto.Content,
                CreatedOn = commentDto.CreatedOn,
                StockId = commentDto.StockId
            };
        }
        public static Comment ToCommentFromCreate(this CommentCreateDto commentDto ,int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }
    }
}
