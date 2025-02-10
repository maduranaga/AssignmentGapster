namespace TvMaze.Application.DTOs
{
    public class TVMazeShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string Premiered { get; set; }
        public List<string> Genres { get; set; }
        public string Summary { get; set; }
    }
}
