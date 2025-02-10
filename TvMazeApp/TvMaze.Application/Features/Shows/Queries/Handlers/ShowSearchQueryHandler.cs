using CodeFirst.Common.Expections;
using MediatR;
using TvMaze.Application.Interfaces.Shows;
using TvMaze.Domain.Entities;

namespace TvMaze.Application.Features.Shows.Queries.Handlers
{
    public class ShowSearchQueryHandler : IRequestHandler<ShowSearchQuery, List<Show>>
    {
        private readonly IShowService _showService;
        public ShowSearchQueryHandler(IShowService showService)
        {
            _showService = showService;
        }
        public async Task<List<Show>> Handle(ShowSearchQuery request, CancellationToken cancellation)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name)) throw new BadRequestException("Name parameter is required");
                var shows = await _showService.GetShowListsByNameAync(request.Name, request.PageSize, request.PageNumber, cancellation);
                return shows;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}