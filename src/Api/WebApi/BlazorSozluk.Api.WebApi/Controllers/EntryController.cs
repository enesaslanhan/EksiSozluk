﻿using BlazorSozluk.Api.Application.Features.Queries.GetEntries;
using BlazorSozluk.Api.Application.Features.Queries.GetEntryComments;
using BlazorSozluk.Api.Application.Features.Queries.GetEntryDetail;
using BlazorSozluk.Api.Application.Features.Queries.GetMainPageEntries;
using BlazorSozluk.Api.Application.Features.Queries.GetUserEntries;
using BlazorSozluk.Common.Models.Queries;
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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetEntryDetailQuery(id,UserId));

            return Ok(result);
        }

        [HttpGet("Comments/{id}")]
        public async Task<IActionResult> GetEntryComments(Guid id,int page,int pageSize)
        {
            var result = await _mediator.Send(new GetEntryCommentsQuery(page,pageSize,id,UserId));

            return Ok(result);
        }

        [HttpGet("UserEntries")]
        public async Task<IActionResult> GetEntries(string userName,Guid userId,int page,int pageSize)
        {
            if (userId == Guid.Empty && string.IsNullOrEmpty(userName))
                userId = UserId.Value;
            var result = await _mediator.Send(new GetUserEntriesQuery(userId,userName,page,pageSize));

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] GetEntiresQuery getEntiresQuery)
        {
            var entries = await _mediator.Send(getEntiresQuery);

            return Ok(entries);
        }

        [HttpGet]
        [Route("MainPageEntries")]
        public async Task<IActionResult> GetMainPageEntries(int page,int pageSize)
        {
            var entries = await _mediator.Send(new GetMainPageEntriesQuery(null,page,pageSize));

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

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
        {
            var entries = await _mediator.Send(query);

            return Ok(entries);
        }
    }
}
