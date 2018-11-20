using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.Repositories;

namespace Tradingcenter.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;

        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetByIdAsync(int commentId)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == commentId);
            return comment;
        }

        public async Task<List<Comment>> GetByPortfolioIdAsync(int portfolioId)
        {
            var commentList = await _context.Comments.Where(x => x.PortfolioId == portfolioId).ToListAsync();
            return commentList.OrderByDescending(x => x.PostedOn).ToList();
        }

        public async Task<List<Comment>> GetByUserIdAsync(int userId)
        {
            var commentList = await _context.Comments.Where(x => x.UserId == userId).ToListAsync();
            return commentList.OrderByDescending(x => x.PostedOn).ToList();
        }
    }
}
