using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonDirectory.API.Models;
using PersonDirectory.Application.Commands;

namespace PersonDirectory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PersonController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddPersonModel model, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<AddPersonCommand>(model);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdatePersonModel model, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdatePersonCommand>(model);
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Update([FromBody] DeletePersonModel model, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<DeletePersonCommand>(model);
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}