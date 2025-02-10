using MediatR;
using TvMaze.Application.Interfaces.Shows;
using TvMaze.Domain.Entities;

namespace TvMaze.Application.Features.Shows.Queries.Handlers
{
    public class GetShowsQueryHandler : IRequestHandler<GetShowsQuery, List<Show>>
    {
        private readonly IShowService _showService;
        public GetShowsQueryHandler(IShowService showService)
        {
            _showService = showService;
        }
        public async Task<List<Show>> Handle(GetShowsQuery request, CancellationToken cancellation)
        {
            try
            {
                var shows = await _showService.GetShowListAsync(request.PageSize,request.PageNumber,cancellation);
                return shows;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
