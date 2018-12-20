using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.OrderCommentDTOs;
using Trainingcenter.Domain.DTOs.PortfolioCommentDTOs;

namespace Trainingcenter.Domain.Services.CommentServices
{
    public interface ICommentServices
    {
        Task<PortfolioCommentDTO> GetPortfolioCommentById(int commentId);
        Task<List<PortfolioCommentDTO>> GetAllPortfolioCommentByUserId(int userId);
        Task<List<PortfolioCommentDTO>> GetAllPortfolioCommentByPortfolioId(int portfolioId);

        Task<PortfolioCommentDTO> CreatePortfolioComment(PortfolioCommentToCreateDTO commentToCreate);
        Task<PortfolioCommentDTO> UpdatePortfolioComment(PortfolioCommentToUpdateDTO commentToUpdate);
        Task<PortfolioCommentDTO> DeletePortfolioComment(int commentId);

        Task<OrderCommentDTO> GetOrderCommentById(int commentId);
        Task<List<OrderCommentDTO>> GetAllOrderCommentByUserId(int userId);
        Task<List<OrderCommentDTO>> GetAllOrderCommentByOrderId(int orderId);

        Task<OrderCommentDTO> CreateOrderComment(OrderCommentToCreateDTO commentToCreate);
        Task<OrderCommentDTO> UpdateOrderComment(OrderCommentToUpdateDTO commentToUpdate);
        Task<OrderCommentDTO> DeleteOrderComment(int commentId);
    }
}
