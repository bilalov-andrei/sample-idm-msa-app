using AutoMapper;
using IDM.EmployeeService.API.Employees;
using IDM.EmployeeService.API.InputModels;
using IDM.EmployeeService.Application.Commands.CreateEmployee;
using IDM.EmployeeService.Application.Handlers.DismissEmployee;
using IDM.EmployeeService.Application.Queries.GetAllEmployees;
using IDM.EmployeeService.Application.Queries.GetEmployeeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IDM.EmployeeService.API.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EmployeeController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeViewModel value, CancellationToken cancellationToken)
        {
            var employeeId = await _mediator.Send(_mapper.Map<CreateEmployeeCommand>(value), cancellationToken);

            return CreatedAtRoute("GetEmployeeById", new { id = employeeId }, null);
        }

        [HttpGet("{id:int}", Name = "GetEmployeeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployeeById(int id, CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(new GetEmployeeByIdQuery(id), cancellationToken);
            if (product is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeeViewModel>(product));
        }

        //TODO implement pagination
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var allElements = await _mediator.Send(new GetAllEmployeesQuery(), cancellationToken);

            return Ok(_mapper.Map<IEnumerable<EmployeeViewModel>>(allElements.Items));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Dismiss([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DismissEmployeeCommand(id), cancellationToken);

            return NoContent();
        }
    }
}
