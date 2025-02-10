using MediatR;
using TvMaze.Domain.Entities;

namespace TvMaze.Application.Features.Shows.Queries
{
    public class GetShowByIdQuery : IRequest<Show?>
    {
        public int Id { get; set; }
    }
}
