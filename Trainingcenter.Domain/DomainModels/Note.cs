using System.ComponentModel.DataAnnotations;

namespace Trainingcenter.Domain.DomainModels
{
    public class Note
    {
        public int NoteId { get; set; }
        public int PortfolioId { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public Portfolio Portfolio { get; set; }
    }
}