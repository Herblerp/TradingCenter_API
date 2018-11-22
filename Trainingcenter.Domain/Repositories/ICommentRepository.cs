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
        Task<Comment> GetByIdAsync(int commentId);

        //Returns all the comments from the portfolio with the given portfolioId
        Task<List<Comment>> GetByPortfolioIdAsync(int portfolioId);

        //Returns all the comments from the user with the given userId
        Task<List<Comment>> GetByUserIdAsync(int userId);
    }
}
