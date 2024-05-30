using Asp.Versioning;
using Goal.Application.Commands;
using Goal.Infra.Http.Controllers;
using Goal.Infra.Http.Controllers.Requests;
using Goal.Infra.Http.Controllers.Results;
using Goal.Infra.Http.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Core.Application.Commands.Customers;
using Nexus.Core.Infra.Data.Query.Repositories.Customers;
using Nexus.Core.Model.Customers;
using Nexus.Infra.Http.Controllers;

namespace Nexus.Core.Api.Controllers.Customers;

[ApiController]
[ApiVersion("1")]
[Authorize("admin")]
[Route("v{version:apiVersion}/[controller]")]
public class CustomersController(
    ICustomerQueryRepository customerQueryRepository,
    IMediator mediator)
    : ApiControllerBase
{
    private readonly ICustomerQueryRepository customerQueryRepository = customerQueryRepository;
    private readonly IMediator mediator = mediator;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<PagedResponse<Customer>>> Get([FromQuery] PageSearchRequest request)
        => Paged(await customerQueryRepository.QueryAsync(request.ToPageSearch()));

    [HttpGet("{id}", Name = $"{nameof(CustomersController)}_{nameof(GetById)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponse))]
    public async Task<ActionResult<Customer>> GetById([FromRoute] string id)
    {
        Customer customer = await customerQueryRepository.LoadAsync(id);

        return customer is null
            ? NotFound()
            : Ok(customer);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse<Customer>>> Post([FromBody] RegisterCustomerRequest request)
    {
        var command = new RegisterCustomerCommand(
            request.Name!,
            request.Email!,
            request.Birthdate);

        ICommandResult<Customer> result = await mediator
            .Send<ICommandResult<Customer>>(command);

        return !result.IsSucceeded
            ? CommandFailure(result)
            : CreatedAtRoute(
                $"{nameof(CustomersController)}_{nameof(GetById)}",
                new { id = result.Data!.CustomerId },
                ApiResponse.FromCommand(result));
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse<Customer>>> Patch([FromRoute] string id, [FromBody] UpdateCustomerRequest request)
    {
        ICommandResult result = await mediator.Send(
            new UpdateCustomerCommand(
                id,
                request.Name!,
                request.Birthdate));

        return result.IsSucceeded
            ? AcceptedAtAction($"{nameof(CustomersController)}_{nameof(GetById)}", new { id }, null)
            : CommandFailure(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse>> Delete([FromRoute] string id)
    {
        ICommandResult result = await mediator.Send(new RemoveCustomerCommand(id));

        return result.IsSucceeded
            ? Accepted()
            : CommandFailure(result);
    }
}
