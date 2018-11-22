using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.CommentDTOs;
using Trainingcenter.Domain.Repositories;

namespace Trainingcenter.Domain.Services.CommentServices
{
    public class CommentServices : ICommentServices
    {
        #region DependencyInjection

        private readonly IGenericRepository _genericRepo;
        private readonly ICommentRepository _commentRepo;

        public CommentServices(IGenericRepository genericRepo, ICommentRepository commentRepo)
        {
            _genericRepo = genericRepo;
            _commentRepo = commentRepo;
        }

        #endregion

        public async Task<CommentDTO> CreateComment(CommentToCreateDTO commentToCreate)
        {
            Comment comment = ConvertCommentToCreate(commentToCreate);
            comment.PostedOn = DateTime.Now.Date;
            return ConvertComment(await _genericRepo.AddAsync(comment));
        }

        public async Task<CommentDTO> DeleteComment(int commentId)
        {
            Comment comment = await _commentRepo.GetByIdAsync(commentId);
            return ConvertComment(await _genericRepo.DeleteAsync(comment));
        }

        public async Task<List<CommentDTO>> GetAllCommentByPortfolioId(int portfolioId)
        {
            List<Comment> commentList = await _commentRepo.GetByPortfolioIdAsync(portfolioId);

            if (commentList == null)
                return null;

            commentList = commentList.OrderByDescending(x => x.PostedOn).ToList();
            List<CommentDTO> commentDTOList = new List<CommentDTO>();

            foreach(Comment comment in commentList)
            {
                commentDTOList.Add(ConvertComment(comment));
            }
            return commentDTOList;
        }

        public async Task<List<CommentDTO>> GetAllCommentByUserId(int userId)
        {
            List<Comment> commentList = await _commentRepo.GetByUserIdAsync(userId);
            
            if(commentList == null)
                return null;

            commentList = commentList.OrderByDescending(x => x.PostedOn).ToList();
            List<CommentDTO> commentDTOList = new List<CommentDTO>();

            foreach(Comment comment in commentList)
            {
                commentDTOList.Add(ConvertComment(comment));
            }
            return commentDTOList;
        }

        public async Task<CommentDTO> GetCommentById(int commentId)
        {
            return ConvertComment(await _commentRepo.GetByIdAsync(commentId));
        }

        public async Task<CommentDTO> UpdateComment(CommentToUpdateDTO x)
        {
            Comment comment = await _commentRepo.GetByIdAsync(x.CommentId);
            comment.Message = x.Message;
            return ConvertComment(await _genericRepo.UpdateAsync(comment));
        }

        #region Converters

        private CommentDTO ConvertComment(Comment x)
        {
            if (x == null)
                return null;

            CommentDTO comment = new CommentDTO
            {
                CommentId = x.CommentId,
                UserId = x.UserId,
                PortfolioId = x.PortfolioId,
                Message = x.Message,
                PostedOn = x.PostedOn.ToString("dd/MM/yyyy")
            };
            return comment;
        }
        private Comment ConvertCommentToCreate(CommentToCreateDTO x)
        {
            if (x == null)
                return null;

            Comment comment = new Comment
            {
                UserId = x.UserId,
                PortfolioId = x.PortfolioId,
                Message = x.Message
            };

            return comment;
        }

        #endregion
    }
}
