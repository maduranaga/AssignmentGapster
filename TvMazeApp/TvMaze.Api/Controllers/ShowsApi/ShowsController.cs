using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TvMaze.Application.Features.Shows.Commands;
using TvMaze.Application.Features.Shows.Queries;

namespace TvMaze.Api.Controllers.ShowsApi
{
    [EnableRateLimiting("fixedPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class ShowsController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<ShowsController> _logger;

        public ShowsController(IMediator mediator, ILogger<ShowsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedShows([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("Fetching shows with pagination - Page: {PageNumber}, Size: {PageSize}", pageNumber, pageSize);

            var pagedShows = await _mediator.Send(new GetShowsQuery { PageNumber=pageNumber, PageSize= pageSize });

            return Ok(pagedShows);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShowById(int id)
        {
            var show = await _mediator.Send(new GetShowByIdQuery { Id = id });
            return Ok(show);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetShowByName([FromQuery] string name, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var show = await _mediator.Send(new ShowSearchQuery { Name = name, PageNumber=pageNumber, PageSize= pageSize });
            return Ok(show);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShow([FromBody] ShowAddCommand command)
        {
            _logger.LogInformation("Add New Show TvMaz list {@command}", command);
            var createdShow = await _mediator.Send(command);
            return Ok(createdShow);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShow([FromBody] ShowUpdateCommand command)
        {
            _logger.LogInformation("Update  Show TvMaz list {@command}", command);
            var success = await _mediator.Send(command);
            return Ok(success);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShow(int id)
        {
            _logger.LogInformation("Delete Show TvMaz {@id}", id);
            var success = await _mediator.Send(new ShowDeleteCommand { Id = id });
            return Ok(success);
        }
    }
}
