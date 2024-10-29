using Asp.Versioning;
using Goal.Infra.Http.Controllers;
using Goal.Infra.Http.Controllers.Requests;
using Goal.Infra.Http.Controllers.Results;
using Goal.Infra.Http.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Core.Application.Customers.Commands;
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
    : ApiController
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
        Customer? customer = await customerQueryRepository.LoadAsync(id);

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
        Customer result = await mediator.Send<Customer>(
            new RegisterCustomerCommand(
                request.Name,
                request.Email,
                request.Birthdate));

        return CreatedAtRoute(
            $"{nameof(CustomersController)}_{nameof(GetById)}",
            new { id = result.CustomerId },
            ApiResponse.Success(result));
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse<Customer>>> Patch([FromRoute] string id, [FromBody] UpdateCustomerRequest request)
    {
        await mediator.Send(
            new UpdateCustomerCommand(
                id,
                request.Name,
                request.Email,
                request.Birthdate));

        return AcceptedAtRoute(
            $"{nameof(CustomersController)}_{nameof(GetById)}",
            new { id },
            null);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse>> Delete([FromRoute] string id)
    {
        await mediator.Send(new RemoveCustomerCommand(id));
        return Accepted();
    }
}
