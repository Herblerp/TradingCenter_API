using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.OrderCommentDTOs;
using Trainingcenter.Domain.DTOs.PortfolioCommentDTOs;
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

        public async Task<PortfolioCommentDTO> CreatePortfolioComment(PortfolioCommentToCreateDTO commentToCreate)
        {
            PortfolioComment comment = ConvertCommentToCreate(commentToCreate);
            comment.PostedOn = DateTime.Now.Date;
            return ConvertComment(await _genericRepo.AddAsync(comment));
        }

        public async Task<OrderCommentDTO> CreateOrderComment(OrderCommentToCreateDTO commentToCreate)
        {
            var comment = ConvertOrderCommentToCreate(commentToCreate);
            return ConvertOrderComment(await _genericRepo.AddAsync(comment));
        }

        public async Task<OrderCommentDTO> DeleteOrderComment(int commentId)
        {
            var comment = await _commentRepo.GetOrderCommentByIdAsync(commentId);
            return ConvertOrderComment(await _genericRepo.DeleteAsync(comment));
        }

        public async Task<PortfolioCommentDTO> DeletePortfolioComment(int commentId)
        {
            PortfolioComment comment = await _commentRepo.GetPortfolioCommentByIdAsync(commentId);
            return ConvertComment(await _genericRepo.DeleteAsync(comment));
        }

        public async Task<List<OrderCommentDTO>> GetAllOrderCommentByOrderId(int orderId)
        {
            var commentList = await _commentRepo.GetOrderCommentByOrderIdAsync(orderId);
            var convertedCommentList = new List<OrderCommentDTO>();

            foreach(OrderComment comment in commentList)
            {
                convertedCommentList.Add(ConvertOrderComment(comment));
            }
            return convertedCommentList;
        }

        public async Task<List<OrderCommentDTO>> GetAllOrderCommentByUserId(int userId)
        {
            var commentList = await _commentRepo.GetOrderCommentByUserIdAsync(userId);

            commentList = commentList.OrderByDescending(x => x.PostedOn).ToList();
            var commentDTOList = new List<OrderCommentDTO>();

            foreach (OrderComment comment in commentList)
            {
                commentDTOList.Add(ConvertOrderComment(comment));
            }
            return commentDTOList;
        }

        public async Task<List<PortfolioCommentDTO>> GetAllPortfolioCommentByPortfolioId(int portfolioId)
        {
            List<PortfolioComment> commentList = await _commentRepo.GetPortfolioCommentByPortfolioIdAsync(portfolioId);

            if (commentList == null)
                return null;

            commentList = commentList.OrderByDescending(x => x.PostedOn).ToList();
            List<PortfolioCommentDTO> commentDTOList = new List<PortfolioCommentDTO>();

            foreach(PortfolioComment comment in commentList)
            {
                commentDTOList.Add(ConvertComment(comment));
            }
            return commentDTOList;
        }

        public async Task<List<PortfolioCommentDTO>> GetAllPortfolioCommentByUserId(int userId)
        {
            List<PortfolioComment> commentList = await _commentRepo.GetPortfolioCommentByUserIdAsync(userId);
            
            if(commentList == null)
                return null;

            commentList = commentList.OrderByDescending(x => x.PostedOn).ToList();
            List<PortfolioCommentDTO> commentDTOList = new List<PortfolioCommentDTO>();

            foreach(PortfolioComment comment in commentList)
            {
                commentDTOList.Add(ConvertComment(comment));
            }
            return commentDTOList;
        }

        public async Task<OrderCommentDTO> GetOrderCommentById(int commentId)
        {
            var comment = ConvertOrderComment(await _commentRepo.GetOrderCommentByIdAsync(commentId));
            return comment;
        }

        public async Task<PortfolioCommentDTO> GetPortfolioCommentById(int commentId)
        {
            return ConvertComment(await _commentRepo.GetPortfolioCommentByIdAsync(commentId));
        }

        public async Task<OrderCommentDTO> UpdateOrderComment(OrderCommentToUpdateDTO commentToUpdate)
        {
            var comment = await _commentRepo.GetOrderCommentByIdAsync(commentToUpdate.CommentId);
            comment.Message = commentToUpdate.Message;

            return ConvertOrderComment(await _genericRepo.UpdateAsync(comment));
        }

        public async Task<PortfolioCommentDTO> UpdatePortfolioComment(PortfolioCommentToUpdateDTO x)
        {
            PortfolioComment comment = await _commentRepo.GetPortfolioCommentByIdAsync(x.CommentId);
            comment.Message = x.Message;
            return ConvertComment(await _genericRepo.UpdateAsync(comment));
        }

        #region Converters

        private PortfolioCommentDTO ConvertComment(PortfolioComment x)
        {
            if (x == null)
                return null;

            PortfolioCommentDTO comment = new PortfolioCommentDTO
            {
                CommentId = x.PortfolioCommentId,
                UserId = x.UserId,
                PortfolioId = x.PortfolioId,
                Message = x.Message,
                PostedOn = x.PostedOn.ToString("dd/MM/yyyy")
            };
            return comment;
        }
        private PortfolioComment ConvertCommentToCreate(PortfolioCommentToCreateDTO x)
        {
            if (x == null)
                return null;

            PortfolioComment comment = new PortfolioComment
            {
                UserId = x.UserId,
                PortfolioId = x.PortfolioId,
                Message = x.Message
            };

            return comment;
        }

        private OrderCommentDTO ConvertOrderComment(OrderComment x)
        {
            if (x == null)
                return null;

            OrderCommentDTO comment = new OrderCommentDTO
            {
                CommentId = x.OrderCommentId,
                UserId = x.UserId,
                OrderId = x.OrderId,
                Message = x.Message,
                PostedOn = x.PostedOn.ToString("dd/MM/yyyy")
            };
            return comment;
        }
        private OrderComment ConvertOrderCommentToCreate(OrderCommentToCreateDTO x)
        {
            if (x == null)
                return null;

            OrderComment comment = new OrderComment
            {
                UserId = x.UserId,
                OrderId = x.OrderId,
                Message = x.Message
            };

            return comment;
        }

        #endregion
    }
}
