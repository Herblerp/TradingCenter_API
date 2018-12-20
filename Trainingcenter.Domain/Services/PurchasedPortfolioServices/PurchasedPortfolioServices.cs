using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.PurchasedPortfolioDTOs;
using Trainingcenter.Domain.Repositories;

namespace Trainingcenter.Domain.Services.PurchasedPortfolioServices
{
    public class PurchasedPortfolioServices : IPurchasedPortfolioServices
    {
        private readonly IGenericRepository _genericRepo;
        private readonly IPurchasedPortfolioRepository _ppRepo;

        public PurchasedPortfolioServices(IGenericRepository genericRepo, IPurchasedPortfolioRepository pprepo)
        {
            _genericRepo = genericRepo;
            _ppRepo = pprepo;
        }

        public async Task<PurchasedPortfolioDTO> AddPurchasedPortfolio(PurchasedPortfolioDTO pp)
        {
            var x = ConvertPurchaseDTO(pp);
            x.PurchasedOn = DateTime.Now;
            var ppCreated = ConvertPurchase(await _genericRepo.AddAsync(x));

            return ppCreated;
        }

        public async Task<bool> Exists(int userId, int portfolioId)
        {
            bool exists = await _ppRepo.Exists(userId, portfolioId);
            return exists;
        }

        public async Task<List<PurchasedPortfolioDTO>> GetPurchasedPortfoliosById(int userId)
        {
            var purchaseList = await _ppRepo.GetPurchasedPortfoliosByUserId(userId);
            var purchaseDTOList = new List<PurchasedPortfolioDTO>();

            foreach(PurchasedPortfolio pp in purchaseList)
            {
                purchaseDTOList.Add(ConvertPurchase(pp));
            }
            return purchaseDTOList;
        }

        public async Task<PurchasedPortfolioDTO> RemovePurchasedPortfolio(PurchasedPortfolioDTO pp)
        {
            if (await Exists(pp.UserId, pp.PortfolioId))
            {
                var purchase = await _ppRepo.GetPurchasedPortfolio(pp.UserId, pp.PortfolioId);
                return ConvertPurchase(await _genericRepo.DeleteAsync(purchase));
            }
            return null;
        }

        private PurchasedPortfolioDTO ConvertPurchase (PurchasedPortfolio pp)
        {
            if(pp == null)
            {
                return null;
            }

            var ppDTO = new PurchasedPortfolioDTO()
            {
                UserId = pp.UserId,
                PortfolioId = pp.PortfolioId
            };
            return ppDTO;
        }

        private PurchasedPortfolio ConvertPurchaseDTO (PurchasedPortfolioDTO ppDTO)
        {
            if(ppDTO == null)
            {
                return null;
            }

            var pp = new PurchasedPortfolio()
            {
                UserId = ppDTO.UserId,
                PortfolioId = ppDTO.PortfolioId
            };
            return pp;
        }

        public async Task<List<SoldPerMonthDTO>> GetSoldPerMonth(int portfolioId)
        {
            var purchases = await _ppRepo.GetPortfolioPurchases(portfolioId);
            purchases = purchases.OrderBy(x => x.PurchasedOn).ToList();

            if(purchases == null)
            {
                return null;
            }

            var currentMonth = purchases[0].PurchasedOn.ToString("MM/yyyy");
            var count = 0;

            var soldPerMonthList = new List<SoldPerMonthDTO>();

            foreach (var spm in purchases)
            {
                var tempDate = spm.PurchasedOn.ToString("MM/yyyy");
                if (tempDate == currentMonth)
                {
                    count++;
                }
                else
                {
                    var SoldPerMonth = new SoldPerMonthDTO
                    {
                        Amount = count,
                        Month = currentMonth
                    };

                    soldPerMonthList.Add(SoldPerMonth);

                    currentMonth = spm.PurchasedOn.ToString("MM/yyyy");
                    count = 1;
                }
            }
            var SoldPerMonthFinal = new SoldPerMonthDTO
            {
                Amount = count,
                Month = currentMonth
            };

            soldPerMonthList.Add(SoldPerMonthFinal);

            return soldPerMonthList;
        }
    }
}
