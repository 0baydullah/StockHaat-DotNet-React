using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
    public class CommentCreateDto 
    {
        [Required]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        [MinLength(4, ErrorMessage = "Title must be at least 4 character long.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Title can only contain alphanumeric characters and spaces.")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(500, ErrorMessage = "Content cannot be longer than 500 characters.")]
        [MinLength(1, ErrorMessage = "Content must be at least 1 characters long.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,!?]+$", ErrorMessage = "Content can only contain alphanumeric characters, spaces, and punctuation.")]
        public string Content { get; set; } = string.Empty;
    }
}
