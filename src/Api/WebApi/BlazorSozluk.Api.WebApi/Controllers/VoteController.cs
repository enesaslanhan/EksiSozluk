using BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteVote;
using BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteVote;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.Common.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BlazorSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : BaseController
    {
        private readonly IMediator _mediator;

        public VoteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("Entry/{entryId}")]
        public async Task<IActionResult> CreateEntryVote(Guid entryId,VoteType voteType=VoteType.UpVote)
        {
            var result = await _mediator.Send(new CreateEntryVoteCommand(entryId,UserId.Value,voteType));
            return Ok(result);
        }

        [HttpPost]
        [Route("EntryComment/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var result = await _mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId, UserId.Value, voteType));
            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryVote/{entrytId}")]
        public async Task<IActionResult> DeleteEntryVote(Guid entryId)
        {
            var result = await _mediator.Send(new DeleteEntryVoteCommand(entryId, UserId.Value));
            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryCommentVote/{entrytCommentId}")]
        public async Task<IActionResult> DeleteEntryCommentVote(Guid entryCommentId)
        {
            var result = await _mediator.Send(new DeleteEntryCommentVoteCommand(entryCommentId, UserId.Value));
            return Ok(result);
        }
    }
}
