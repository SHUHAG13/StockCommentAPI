using Microsoft.EntityFrameworkCore;
using StockComment.Data;
using StockComment.Interfaces;
using StockComment.Models;
using StockComment.Models.Dtos;

namespace StockComment.Repository
{
    public class CommentRepository : ICommentInterface
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }
        
        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        
        public async Task<Comment> UpdateCommentAsync(int id, UpdateCommentRequestDto commentModel)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment==null)
            {
                return null;
            }
            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;
            existingComment.CreatedOn = commentModel.CreatedOn;
            existingComment.StockId = commentModel.StockId;
            await _context.SaveChangesAsync();
            return existingComment;
        }

        public async Task<Comment> DeleteAsync(int id)
        {
            var commentModel = await _context.Comments.FindAsync(id);
            if (commentModel == null)
            {
                return null;
            }
            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }
    }
}
