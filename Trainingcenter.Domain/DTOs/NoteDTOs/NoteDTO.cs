using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.Domain.DTOs.NoteDTOs
{
    public class NoteDTO
    {
        public int NoteId { get; set; }
        public int PortfolioId { get; set; }

        public string Message { get; set; }
    }
}
