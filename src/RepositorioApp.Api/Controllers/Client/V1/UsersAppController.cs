using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositorioApp.Api._Config.FiltersAndAttributes;
using RepositorioApp.Api._Config.Swagger.ResponsesAttributes;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.Domain.AppServices.Users;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Contracts.Infra;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Projections;
using RepositorioApp.Domain.ViewsModels;
using RepositorioApp.Utilities.Results;
namespace RepositorioApp.Api.Controllers.Client.V1
{
    [CustomRoute(AppConstants.ClientApiV1Key, "users-app")]
    public class UsersAppController : BaseClientV1Controller
    {
        private readonly ISessionProvider _sessionProvider;
        private readonly IUserRepository _userRepository;

        public UsersAppController(
            IUserRepository userRepository,
            ISessionProvider sessionProvider)
        {
            _userRepository = userRepository;
            _sessionProvider = sessionProvider;
        }

        [OkResponseType(typeof(EnvelopDataResult<UserVm>))]
        [BadRequestResponseTypeType] [InternalServerErrorResponseType]
        [Authorize(AppPolices.Authenticated)]
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var user = _userRepository.ListAsNoTracking(u => u.Id == _sessionProvider.Id)
                .ToVm()
                .FirstOrDefault();

            return await Task.FromResult(user is null ? NotFoundResponse() : OkResponse(user));
        }

        [OkResponseType(typeof(EnvelopDataResult<UserVm>))]
        [BadRequestResponseTypeType] [InternalServerErrorResponseType]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] ICreateUserAppService service,
            [FromBody] CreateUserCmd command)
        {
            return command == null
                ? UnprocessableEntityResponse()
                : CreatedResponse(await service.Create(command));
        }

        [OkResponseType(typeof(EnvelopDataResult<UserVm>))]
        [BadRequestResponseTypeType] [InternalServerErrorResponseType]
        [Authorize(AppPolices.Authenticated)]
        [HttpPatch("update-accepted-term")]
        public async Task<IActionResult> UpdateAcceptedTerm(
            [FromServices] IUpdateUserAppService service, 
            [FromBody] UpdateAcceptedTermCmd command)
        {
            if (command == null) return UnprocessableEntityResponse();

            command.RequesterUserId = _sessionProvider.Id;
            return CreatedResponse(await service.UpdateAcceptedTerm(command));
        }

        [OkResponseType(typeof(EnvelopDataResult<UserVm>))]
        [BadRequestResponseTypeType] [InternalServerErrorResponseType]
        [Authorize(AppPolices.Authenticated)]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(
            [FromServices] IUpdateUserAppService service,
            [FromBody] UpdateProfileCmd command)
        {
            if (command == null) return UnprocessableEntityResponse();

            command.Id = _sessionProvider.Id;
            return CreatedResponse(await service.Update(command));
        }
    }
}
