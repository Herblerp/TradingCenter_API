using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Repositories
{
    public interface ICommentRepository
    {
        //Returns the comment with the given Id
        Task<PortfolioComment> GetPortfolioCommentByIdAsync(int commentId);

        //Returns all the comments from the portfolio with the given portfolioId
        Task<List<PortfolioComment>> GetPortfolioCommentByPortfolioIdAsync(int portfolioId);

        //Returns all the comments from the user with the given userId
        Task<List<PortfolioComment>> GetPortfolioCommentByUserIdAsync(int userId);

        //Returns the comment with the given Id
        Task<OrderComment> GetOrderCommentByIdAsync(int commentId);

        //Returns all the comments from the portfolio with the given portfolioId
        Task<List<OrderComment>> GetOrderCommentByOrderIdAsync(int portfolioId);

        //Returns all the comments from the user with the given userId
        Task<List<OrderComment>> GetOrderCommentByUserIdAsync(int userId);
    }
}
