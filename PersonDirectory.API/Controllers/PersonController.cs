using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonDirectory.API.FilterModels;
using PersonDirectory.API.Models;
using PersonDirectory.Application.Commands;
using PersonDirectory.Application.Dtos;
using PersonDirectory.Application.Queries;
using System.ComponentModel.DataAnnotations;

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

        #region Command

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

        [HttpPost("AddRelation")]
        public async Task<IActionResult> AddRelation([FromBody] AddPersonRelationModel model, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<AddPersonRelationCommand>(model);
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete("deleteRelation")]
        public async Task<IActionResult> DeleteRelation([FromBody] DeletePersonRelationModel model, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<DeletePersonRelationCommand>(model);
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }
        #endregion

        #region Query

        [HttpGet("get")]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<PersonDto>> Get([FromQuery][Required] int id, CancellationToken cancellationToken)
        {
            var query = new GetByIdQuery { Id = id };
            var personDto = await _mediator.Send(query, cancellationToken);
            return Ok(personDto);
        }

        [HttpGet("getAll")]
        [ProducesResponseType(typeof(PersonListItemDtoWithTotalCount), StatusCodes.Status200OK)]
        public async Task<ActionResult<PersonListItemDtoWithTotalCount>> GetAll([FromQuery] PersonFilterModel filterModel, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetAllQuery>(filterModel);
            var personListItemDtoWithTotalCount = await _mediator.Send(query, cancellationToken);
            return Ok(personListItemDtoWithTotalCount);
        }

        [HttpGet("report")]
        [ProducesResponseType(typeof(List<RelatedPersonsReportDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<RelatedPersonsReportDto>>> GetRelationsReport(CancellationToken cancellationToken)
        {
            var query = new GetRelationsReportQuery();
            var report = await _mediator.Send(query, cancellationToken);
            return Ok(report);
        }

        #endregion
    }
}