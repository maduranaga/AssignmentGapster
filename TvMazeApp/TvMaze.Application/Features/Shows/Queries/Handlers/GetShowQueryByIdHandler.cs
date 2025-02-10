using CodeFirst.Common.Expections;
using MediatR;
using TvMaze.Application.Interfaces.Shows;
using TvMaze.Domain.Entities;

namespace TvMaze.Application.Features.Shows.Queries.Handlers
{
    public class GetShowQueryByIdHandler : IRequestHandler<GetShowByIdQuery, Show?>
    {
        private readonly IShowService _showService;
        public GetShowQueryByIdHandler(IShowService showService)
        {
            _showService = showService;
        }
        public async Task<Show?> Handle(GetShowByIdQuery request, CancellationToken cancellation)
        {
            try
            {
                if (request.Id<=0) throw new BadRequestException("Invalid Show Id");
                var shows = await _showService.GetShowByIdAsync(request.Id, cancellation);
                if (shows==null) throw new NotFoundException("Invalid Show Id");
                return shows;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}