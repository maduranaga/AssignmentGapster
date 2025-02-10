namespace TvMaze.Domain.Entities
{
    public class Show
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public DateTime Premiered { get; set; }
        public string Summary { get; set; }

        public List<ShowGenre> ShowGenres { get; set; } = new List<ShowGenre>();
    }
}
