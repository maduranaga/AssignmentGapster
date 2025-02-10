using MediatR;

namespace TvMaze.Application.Features.Shows.Commands
{
    public class ShowAddCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Languge { get; set; }
        public DateTime Premiered { get; set; }
        public List<string> Genres { get; set; }
        public string Summary { get; set; }
    }
}
