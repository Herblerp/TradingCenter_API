namespace Trainingcenter.Domain.Entities
{
    public class Picture
    {
        public int PictureId { get; set; }
        public int UserId { get; set; }

        public string URL { get; set; }

        public User User { get; set; }
    }
}