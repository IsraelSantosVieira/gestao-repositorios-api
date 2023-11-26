using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositorioApp.Api._Config.FiltersAndAttributes;
using RepositorioApp.Api._Config.Swagger.ResponsesAttributes;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.Domain.AppServices.Parameter;
using RepositorioApp.Domain.AppServices.Parameter.Commands;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Filters;
using RepositorioApp.Domain.Projections;
using RepositorioApp.Domain.ViewsModels;
using RepositorioApp.Utilities.Paging;
using RepositorioApp.Utilities.Results;
namespace RepositorioApp.Api.Controllers.Client.V1
{
    [CustomRoute(AppConstants.ClientApiV1Key, "parameter")]
    [Authorize(AppPolices.Master)]
    public class ParameterController : BaseClientV1Controller
    {
        private readonly IParameterRepository _parameterRepository;

        public ParameterController(IParameterRepository parameterRepository)
        {
            _parameterRepository = parameterRepository;
        }

        [OkResponseType(typeof(EnvelopDataResult<PagedList<ParameterVm>>))]
        [BadRequestResponseTypeType, InternalServerErrorResponseType]
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ParameterFilter filter)
        {
            var where = _parameterRepository.Where(filter);

            var parameterContents = new PagedList<ParameterVm>(
                _parameterRepository.ListAsNoTracking(where, filter).ToVm(),
                await _parameterRepository.CountAsync(where),
                filter.PageSize
            );

            return OkResponse(parameterContents);
        }
    
        [OkResponseType(typeof(EnvelopDataResult<ParameterVm>))]
        [BadRequestResponseTypeType, InternalServerErrorResponseType]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == Guid.Empty) return NotFoundResponse();
            
            var pagedList = await _parameterRepository.FindAsNoTrackingAsync(m => m.Id == id);
            return pagedList == null ? NotFoundResponse() : OkResponse(pagedList.ToVm());
        }
    
        [OkResponseType(typeof(EnvelopDataResult<ParameterVm>))]
        [BadRequestResponseTypeType, InternalServerErrorResponseType]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] ICreateParameterAppService service, 
            [FromBody] CreateParameterCmd command)
        {
            return command == null
                ? UnprocessableEntityResponse()
                : CreatedResponse(await service.Create(command));
        }
    
        [OkResponseType(typeof(EnvelopDataResult<ParameterVm>))]
        [BadRequestResponseTypeType, InternalServerErrorResponseType]
        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateParameterAppService service, 
            [FromBody] UpdateParameterCmd command,
            [FromRoute] Guid id)
        {
            if (id == Guid.Empty) return NotFoundResponse();
            if (command == null) return UnprocessableEntityResponse();
            
            command.Id = id;

            return OkResponse(await service.Update(command));
        }
    
        [OkResponseType(typeof(EnvelopDataResult<NoContentResult>))]
        [BadRequestResponseTypeType, InternalServerErrorResponseType]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(
            [FromServices] IRemoveParameterAppService service, 
            [FromRoute] Guid id)
        {
            if (id == Guid.Empty) 
                return NotFoundResponse();
            
            await service.Remove(id);
            return NoContent();
        }

        [OkResponseType(typeof(EnvelopDataResult<bool>))]
        [BadRequestResponseTypeType, InternalServerErrorResponseType]
        [HttpPatch("{id:guid}/update-status")]
        public async Task<IActionResult> UpdateStatus(
            [FromServices] IUpdateParameterAppService service,
            [FromRoute] Guid id)
        {
            return id == Guid.Empty
                ? NotFoundResponse()
                : OkResponse(await service.UpdateStatus(id));
        }
    }
}