using StockComment.Models;
using StockComment.Models.Dtos;
namespace StockComment.Interfaces
{
    public interface ICommentInterface
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment> GetByIdAsync(int id);
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<Comment> UpdateCommentAsync(int id, UpdateCommentRequestDto commentModel);
        Task<Comment> DeleteAsync(int id);
    }
}
