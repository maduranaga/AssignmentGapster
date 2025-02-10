using CodeFirst.Common.Expections;
using FluentAssertions;
using Moq;
using TvMaze.Application.Features.Shows.Queries;
using TvMaze.Application.Features.Shows.Queries.Handlers;
using TvMaze.Application.Interfaces.Shows;
using TvMaze.Domain.Entities;

namespace TvMaze.Test.Application.Shows
{
    public class GetShowQueryByIdHandlerTests
    {
        private readonly Mock<IShowService> _mockShowService;
        private readonly GetShowQueryByIdHandler _handler;

        public GetShowQueryByIdHandlerTests()
        {
            _mockShowService = new Mock<IShowService>();
            _handler = new GetShowQueryByIdHandler(_mockShowService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnShow_WhenShowExists()
        {
            var showId = 1;
            var show = new Show { Id = showId, Name = "Breaking Bad" };
            _mockShowService.Setup(s => s.GetShowByIdAsync(showId, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(show);

            var request = new GetShowByIdQuery { Id = showId };

            var result = await _handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(showId);
            result.Name.Should().Be("Breaking Bad");

            _mockShowService.Verify(s => s.GetShowByIdAsync(showId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowBadRequestException_WhenIdIsZeroOrNegative()
        {
            var request = new GetShowByIdQuery { Id = 0 };

            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            await act.Should().ThrowAsync<BadRequestException>()
                     .WithMessage("Invalid Show Id");

            _mockShowService.Verify(s => s.GetShowByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenShowDoesNotExist()
        {
            var showId = 99;
            _mockShowService.Setup(s => s.GetShowByIdAsync(showId, It.IsAny<CancellationToken>()))
                            .ReturnsAsync((Show?)null);

            var request = new GetShowByIdQuery { Id = showId };

            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>()
                     .WithMessage("Invalid Show Id");

            _mockShowService.Verify(s => s.GetShowByIdAsync(showId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}