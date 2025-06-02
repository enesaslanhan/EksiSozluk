using BlazorSozluk.Api.Application.Features.Queries.GetEntries;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : BaseController
    {
        private readonly IMediator _mediator;

        public EntryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] GetEntiresQuery getEntiresQuery)
        {
            var entries = await _mediator.Send(getEntiresQuery);

            return Ok(entries);
        }

        [HttpPost]
        [Route("CreateEntry")]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand command)
        {
            if (!command.CreateById.HasValue)
                command.CreateById = UserId;
            var result= await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost]
        [Route("CreateEntryComment")]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand command)
        {
            if (!command.CreatedById.HasValue)
                command.CreatedById = UserId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
