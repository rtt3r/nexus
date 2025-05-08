using Asp.Versioning;
using Goal.Infra.Crosscutting.Adapters;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Http.Controllers.Requests;
using Goal.Infra.Http.Controllers.Results;
using Goal.Infra.Http.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Hcm.Api.Features.Employees.GetEmployee;
using Nexus.Hcm.Api.Features.Employees.SearchEmployees;
using Nexus.Hcm.Infra.Data.Query.Repositories.People;
using Nexus.Hcm.Model.People;
using Nexus.Infra.Http.Controllers;

namespace Nexus.Hcm.Api.Features.Employees;

[ApiController]
[ApiVersion("1")]
[Authorize("admin")]
[Route("v{version:apiVersion}/[controller]")]
public class EmployeesController(
    IEmployeeQueryRepository employeeQueryRepository,
    ITypeAdapter typeAdapter,
    IMediator mediator)
    : NexusApiController
{
    private readonly IEmployeeQueryRepository employeeQueryRepository = employeeQueryRepository;
    private readonly ITypeAdapter typeAdapter = typeAdapter;
    private readonly IMediator mediator = mediator;

    private const string GET_BY_ID_ROUTE = $"{nameof(EmployeesController)}.{nameof(GetById)}";

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<PagedResponse<SearchEmployeeResponse>>> Get([FromQuery] PageSearchRequest request)
    {
        IPagedList<Employee> response = await employeeQueryRepository.QueryAsync(request.ToPageSearch());
        return Paged(typeAdapter.Adapt<IPagedList<SearchEmployeeResponse>>(response));
    }

    [HttpGet("{id}", Name = GET_BY_ID_ROUTE)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<GetEmployeeResponse>> GetById([FromRoute] string id)
    {
        Employee? employee = await employeeQueryRepository.LoadAsync(id);

        return employee is null
            ? NotFound()
            : Ok(typeAdapter.Adapt<GetEmployeeResponse>(employee));
    }
}
