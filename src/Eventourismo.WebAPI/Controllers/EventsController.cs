using Microsoft.AspNetCore.Mvc;
using MediatR;
using Eventourismo.Application.DTOs;
using Eventourismo.Application.UseCases.Events;

namespace Eventourismo.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<EventDto>>>> GetEvents([FromQuery] GetEventsQuery query)
    {
        try
        {
            var events = await _mediator.Send(query);
            return Ok(ApiResponse<List<EventDto>>.SuccessResult(events));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<List<EventDto>>.FailureResult(ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<EventDto>>> CreateEvent([FromBody] CreateEventCommand command)
    {
        try
        {
            var eventDto = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEventById), new { id = eventDto.Id }, ApiResponse<EventDto>.SuccessResult(eventDto));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<EventDto>.FailureResult(ex.Message));
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<EventDto>>> GetEventById(Guid id)
    {
        try
        {
            // This would need to be implemented as a query
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<EventDto>.FailureResult(ex.Message));
        }
    }
}