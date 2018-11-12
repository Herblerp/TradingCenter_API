using System.ComponentModel.DataAnnotations;

namespace Trainingcenter.Domain.DomainModels
{
    public class Picture
    {
        public int PictureId { get; set; }
        public int UserId { get; set; }

        [Required]
        public string URL { get; set; }

        public User User { get; set; }
    }
}