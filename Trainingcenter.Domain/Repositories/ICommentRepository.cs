using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Repositories
{
    interface ICommentRepository
    {
        Task<Comment> GetFromIdAsync(int commentId);

        Task<List<Comment>> GetAllFromOrderIdAsync(int orderId);
    }
}
