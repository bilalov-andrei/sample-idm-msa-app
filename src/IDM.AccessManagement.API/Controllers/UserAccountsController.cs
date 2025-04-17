using IDM.AccessManagement.Application.Queries.GetUserAccounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IDM.AccessManagement.API.Controllers
{
    [Route("api/useraccounts")]
    [ApiController]
    public class UserAccountsController : Controller
    {
        private readonly IMediator _mediator;

        public UserAccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserAccounts([FromQuery] int? systemId, [FromQuery] int? employeeId, CancellationToken cancellationToken)
        {
            var userAccounts = await _mediator.Send(new GetUserAccountsQuery(employeeId, systemId), cancellationToken);

            return Ok(userAccounts);
        }
    }
}
