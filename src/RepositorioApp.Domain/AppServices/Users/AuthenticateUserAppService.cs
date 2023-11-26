using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.CrossCutting.Models;
using RepositorioApp.CrossCutting.Utils;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Domain.Results;
using RepositorioApp.Jwt.Contracts;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.AppServices.Users
{
    public class AuthenticateUserAppService : BaseAppService, IAuthenticateUserAppService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public AuthenticateUserAppService(
            IUnitOfWork uow,
            IUserRepository userRepository,
            IJwtService jwtService) : base(uow)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<bool> CanAuthenticateSecureUser(AuthenticateUserCmd command)
        {
            User user = await _userRepository.FindAsync(x => x.Email == command.Email);
            return user != null;
        }

        public async Task<AuthenticateUserResult> Authenticate(AuthenticateUserCmd command)
        {
            return command.GrantType switch
            {
                GrantTypes.Password => await HandleGrantTypePassword(command),
                GrantTypes.RefreshToken => await HandleGrantTypeRefreshToken(command),
                _ => throw new DomainException(AppMessages.InvalidScope)
            };
        }

        public async Task<JwToken> IntegrationAuthenticate(AuthenticateUserCmd command)
        {
            var authUser = await Authenticate(command);
            return new JwToken
            {
                AccessToken = authUser.AccessToken,
                RefreshToken = authUser.RefreshToken,
                ExpiresIn = authUser.ExpiresIn,
                NotBefore = authUser.NotBefore
            };
        }

        private async Task<AuthenticateUserResult> HandleGrantTypePassword(AuthenticateUserCmd command)
        {
            var result = new AuthenticateUserResult();

            var user = await _userRepository.FindCompleteByEmailAsync(command.Email);

            if (user == null)
            {
                throw new DomainException(AppMessages.InvalidCredentials);
            }

            if (!user.CanAuthenticate(command.Password, out ICollection<string> errors))
            {
                throw new DomainException(errors);
            }

            result.User = FillSessionUser(user);

            if (result.User != null)
            {
                return await GenerateAccessToken(result, user);
            }

            result.Errors.Add(AppMessages.InvalidCredentials);

            return result;
        }

        private async Task<AuthenticateUserResult> HandleGrantTypeRefreshToken(AuthenticateUserCmd command)
        {
            var result = new AuthenticateUserResult();

            var tokenValidation = await _jwtService.ValidateTokenAsync(command.RefreshToken);

            if (!tokenValidation.IsValid)
            {
                throw new DomainException(UserMessages._RefreshToken.ExpiredError);
            }

            var claimEmail = tokenValidation.ClaimsIdentity.Claims.GetEmail();

            if (!string.Equals(claimEmail, command.Email, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new DomainException(UserMessages._RefreshToken.ExpiredError);
            }

            var user = await _userRepository.FindCompleteByEmailAsync(command.Email);

            if (user == null)
            {
                throw new DomainException(AppMessages.InvalidCredentials);
            }

            result.User = FillSessionUser(user);
            return await GenerateAccessToken(result, user);
        }

        private SessionUser FillSessionUser(User user)
        {
            return SessionUser.GetSessionUser(_userRepository.FindAsNoTracking(x => x.Id == user.Id));
        }

        private async Task<AuthenticateUserResult> GenerateAccessToken(AuthenticateUserResult authenticatedUserResult, User user)
        {
            var claims = new[]
            {
                new Claim(CustomClaims.Email, user.Email)
            };

            var token = await
                _jwtService.GenerateTokenAsync(
                    authenticatedUserResult.User,
                    authenticatedUserResult.User.Identity,
                    identityForRefreshToken: new ClaimsIdentity(claims));

            authenticatedUserResult.AccessToken = token.AccessToken;
            authenticatedUserResult.ExpiresIn = token.ExpiresIn;
            authenticatedUserResult.NotBefore = token.NotBefore;
            authenticatedUserResult.RefreshToken = token.RefreshToken;
            return authenticatedUserResult;
        }
    }

    public interface IAuthenticateUserAppService
    {
        Task<AuthenticateUserResult> Authenticate(AuthenticateUserCmd command);

        Task<JwToken> IntegrationAuthenticate(AuthenticateUserCmd command);

        Task<bool> CanAuthenticateSecureUser(AuthenticateUserCmd command);
    }
}
