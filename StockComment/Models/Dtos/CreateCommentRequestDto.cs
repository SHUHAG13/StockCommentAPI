﻿using System.ComponentModel.DataAnnotations;

namespace StockComment.Models.Dtos
{
    public class CreateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Title cant not be over 280 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Content cant not be over 280 characters")]
        public string Content { get; set; } = string.Empty;
        
    }
}
