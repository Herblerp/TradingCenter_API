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

        public async Task<OrderComment> GetOrderCommentByIdAsync(int commentId)
        {
            var comment = await _context.OrderComments.FirstOrDefaultAsync(x => x.OrderCommentId == commentId);
            return comment;
        }

        public async Task<List<OrderComment>> GetOrderCommentByOrderIdAsync(int orderId)
        {
            var commentList = await _context.OrderComments.Where(x => x.OrderId == orderId).ToListAsync();
            return commentList.OrderByDescending(x => x.PostedOn).ToList();
        }

        public async Task<List<OrderComment>> GetOrderCommentByUserIdAsync(int userId)
        {
            var commentList = await _context.OrderComments.Where(x => x.UserId == userId).ToListAsync();
            return commentList.OrderByDescending(x => x.PostedOn).ToList();
        }

        public async Task<PortfolioComment> GetPortfolioCommentByIdAsync(int commentId)
        {
            var comment = await _context.PortfolioComments.FirstOrDefaultAsync(x => x.PortfolioCommentId == commentId);
            return comment;
        }

        public async Task<List<PortfolioComment>> GetPortfolioCommentByPortfolioIdAsync(int portfolioId)
        {
            var commentList = await _context.PortfolioComments.Where(x => x.PortfolioId == portfolioId).ToListAsync();
            return commentList.OrderByDescending(x => x.PostedOn).ToList();
        }

        public async Task<List<PortfolioComment>> GetPortfolioCommentByUserIdAsync(int userId)
        {
            var commentList = await _context.PortfolioComments.Where(x => x.UserId == userId).ToListAsync();
            return commentList.OrderByDescending(x => x.PostedOn).ToList();
        }
    }
}
