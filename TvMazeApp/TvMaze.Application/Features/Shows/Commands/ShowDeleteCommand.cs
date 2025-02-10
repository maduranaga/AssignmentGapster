using MediatR;

namespace TvMaze.Application.Features.Shows.Commands
{
    public class ShowDeleteCommand : IRequest<bool>
    {
        public int Id { get; set; } 
    }
}
