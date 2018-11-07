using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Interfaces
{
    interface IPictureRepository
    {
        Task<Picture> GetFromIdAsync(int pictureId);
        Task<Picture> GetFromUserIdAsync(int userId);
    }
}
