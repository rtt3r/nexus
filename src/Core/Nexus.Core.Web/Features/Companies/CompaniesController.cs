using Asp.Versioning;
using Goal.Infra.Crosscutting.Adapters;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Http.Controllers.Requests;
using Goal.Infra.Http.Controllers.Results;
using Goal.Infra.Http.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Core.Infra.Data.Query.Repositories.Companies;
using Nexus.Core.Model.Companies;
using Nexus.Core.Web.Features.Companies.CreateCompany;
using Nexus.Core.Web.Features.Companies.GetCompany;
using Nexus.Core.Web.Features.Companies.SearchCompanies;
using Nexus.Infra.Crosscutting.Errors;
using Nexus.Infra.Http.Controllers;
using OneOf;

namespace Nexus.Core.Web.Features.Companies;

[ApiController]
[ApiVersion("1")]
[Authorize("admin")]
[Route("v{version:apiVersion}/[controller]")]
public class CompaniesController(
    ICompanyQueryRepository companyQueryRepository,
    ITypeAdapter typeAdapter,
    IMediator mediator)
    : NexusApiController
{
    private readonly ICompanyQueryRepository companyQueryRepository = companyQueryRepository;
    private readonly ITypeAdapter typeAdapter = typeAdapter;
    private readonly IMediator mediator = mediator;

    private const string GET_BY_ID_ROUTE = $"{nameof(CompaniesController)}.{nameof(GetById)}";

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<PagedResponse<SearchCompanyResponse>>> Get([FromQuery] PageSearchRequest request)
    {
        IPagedList<Company> response = await companyQueryRepository.QueryAsync(request.ToPageSearch());
        return Paged(typeAdapter.Adapt<IPagedList<SearchCompanyResponse>>(response));
    }

    [HttpGet("{id}", Name = GET_BY_ID_ROUTE)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<Company>> GetById([FromRoute] string id)
    {
        Company? company = await companyQueryRepository.LoadAsync(id);

        return company is null
            ? NotFound()
            : Ok(typeAdapter.Adapt<GetCompanyResponse>(company));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse<Company>>> Post([FromBody] CreateCompanyRequest request)
    {
        OneOf<Company, AppError> result = await mediator.Send<OneOf<Company, AppError>>(request.ToCommand());

        return result
            .Match<ActionResult<ApiResponse<Company>>>(
                company => CreatedAtRoute(
                    GET_BY_ID_ROUTE,
                    new { id = company.CompanyId },
                    ApiResponse.Success(typeAdapter.Adapt<CreateCompanyResponse>(company))),
                error => Error(error)
            );
    }

    //[HttpPatch("{id}")]
    //[ProducesResponseType(StatusCodes.Status202Accepted)]
    //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    //[ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ApiResponse))]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    //public async Task<ActionResult<ApiResponse>> Patch([FromRoute] string id, [FromBody] UpdateCompanyRequest request)
    //{
    //    OneOf<None, AppError> result = await mediator.Send<OneOf<None, AppError>>(request.ToCommand(id));

    //    return result
    //       .Match<ActionResult<ApiResponse>>(
    //           none => AcceptedAtRoute(GET_BY_ID_ROUTE, new { id }, ApiResponse.Success()),
    //           error => Error(error)
    //       );
    //}

    //[HttpDelete("{id}")]
    //[ProducesResponseType(StatusCodes.Status202Accepted)]
    //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    //[ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ApiResponse))]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    //public async Task<ActionResult<ApiResponse>> Delete([FromRoute] string id)
    //{
    //    OneOf<None, AppError> result = await mediator.Send<OneOf<None, AppError>>(new DeleteCompanyCommand { Id = id });

    //    return result
    //       .Match<ActionResult<ApiResponse>>(
    //           none => Accepted(ApiResponse.Success()),
    //           error => Error(error)
    //       );
    //}
}
