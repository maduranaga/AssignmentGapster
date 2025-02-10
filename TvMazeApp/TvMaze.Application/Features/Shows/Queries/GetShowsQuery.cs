using MediatR;
using TvMaze.Domain.Entities;

namespace TvMaze.Application.Features.Shows.Queries
{
    public class GetShowsQuery : IRequest<List<Show>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
