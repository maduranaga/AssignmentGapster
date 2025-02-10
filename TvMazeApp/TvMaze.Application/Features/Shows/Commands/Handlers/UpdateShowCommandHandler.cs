using AutoMapper;
using MediatR;
using TvMaze.Application.Interfaces.Shows;
using TvMaze.Domain.Entities;

namespace TvMaze.Application.Features.Shows.Commands.Handlers
{
    public class UpdateShowCommandHandler : IRequestHandler<ShowUpdateCommand, bool>
    {
        private readonly IShowService _showService;
        private readonly IMapper _autoMapper;
        public UpdateShowCommandHandler(IShowService showService, IMapper autoMapper)
        {
            _showService = showService;
            _autoMapper = autoMapper;
        }
        public async Task<bool> Handle(ShowUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var showEntity = _autoMapper.Map<Show>(request);
                var shows = await _showService.UpdateShowAsync(showEntity, request.Genres, cancellationToken);
                return shows;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}