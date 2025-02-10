using CodeFirst.Common.Expections;
using MediatR;
using TvMaze.Application.Interfaces.Shows;

namespace TvMaze.Application.Features.Shows.Commands.Handlers
{
    public class DeleteShowCommandHandler : IRequestHandler<ShowDeleteCommand, bool>
    {
        private readonly IShowService _showService;
        public DeleteShowCommandHandler(IShowService showService)
        {
            _showService = showService;
        }
        public async Task<bool> Handle(ShowDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id<0) throw new BadRequestException("Id is Invalid");
                var shows = await _showService.DeleteShowAsync(request.Id, cancellationToken);
                return shows;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}