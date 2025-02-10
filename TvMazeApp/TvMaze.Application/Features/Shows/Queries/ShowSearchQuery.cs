using MediatR;
using TvMaze.Domain.Entities;

namespace TvMaze.Application.Features.Shows.Queries
{
    public class ShowSearchQuery : IRequest<List<Show>>
    {
        public string Name { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
