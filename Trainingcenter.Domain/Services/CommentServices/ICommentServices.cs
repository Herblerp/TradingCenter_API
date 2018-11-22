using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.CommentDTOs;

namespace Trainingcenter.Domain.Services.CommentServices
{
    public interface ICommentServices
    {
        Task<CommentDTO> GetCommentById(int commentId);
        Task<List<CommentDTO>> GetAllCommentByUserId(int userId);
        Task<List<CommentDTO>> GetAllCommentByPortfolioId(int portfolioId);

        Task<CommentDTO> CreateComment(CommentToCreateDTO commentToCreate);
        Task<CommentDTO> UpdateComment(CommentToUpdateDTO commentToUpdate);
        Task<CommentDTO> DeleteComment(int commentId);
    }
}
