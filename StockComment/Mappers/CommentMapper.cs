using StockComment.Models;
using StockComment.Models.Dtos;
namespace StockComment.Mappers
{
    public  static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
              Id=commentModel.Id,
              Title=commentModel.Title,
              Content=commentModel.Content,
              CreatedOn=commentModel.CreatedOn,
              StockId=commentModel.StockId,
            };
        }

        public static Comment ToCommentFromCreateDto(this CreateCommentRequestDto createCommentDto)
        {
            return new Comment
            {
                Title = createCommentDto.Title,
                Content = createCommentDto.Content,
                CreatedOn = createCommentDto.CreatedOn,
                StockId = createCommentDto.StockId,
            };
        }
    }
}
