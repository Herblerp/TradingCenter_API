namespace Trainingcenter.Domain.DomainModels
{
    public class Note
    {
        public int NoteId { get; set; }
        public int PortfolioId { get; set; }

        public string Message { get; set; }

        public Portfolio Portfolio { get; set; }
    }
}