using AutoMapper;
using CodeFirst.Common.Expections;
using MediatR;
using TvMaze.Application.Interfaces.Shows;
using TvMaze.Domain.Entities;

namespace TvMaze.Application.Features.Shows.Commands.Handlers
{
    public class AddShowCommandHandler : IRequestHandler<ShowAddCommand, bool>
    {
        private readonly IMapper _autoMapper;
        private readonly IShowService _showService;
        public AddShowCommandHandler(IShowRepository showRepository, IMapper mapper, IShowService showService)
        {
            _autoMapper = mapper;
            _showService=showService;
        }
        public async Task<bool> Handle(ShowAddCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var addShow = _autoMapper.Map<Show>(request);
                var checkExist = await _showService.AddShowIfNotExistsAsync(addShow, request.Genres, cancellationToken)
                    .ConfigureAwait(false);
                return true;
            }
            catch (BadRequestException ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}