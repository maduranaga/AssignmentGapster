namespace TvMaze.Domain.Entities
{
    public class ShowGenre
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public string Genre { get; set; }
        public Show Show { get; set; }
    }
}
