using System;
using System.Collections.Generic;
using System.Linq;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.Domain.Contracts.Metadata;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Security;
namespace RepositorioApp.Domain.Entities
{
    public class User : IEntity
    {
        public User(
            string email,
            string firstName,
            string phone,
            string lastName = null,
            string avatar = null)
        {
            Id = Guid.NewGuid();
            Phone = phone;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Avatar = avatar;
            
            Active = true;
            AcceptedTerm = true;
            PendingRegisterInformation = true;
            CreatedAt = DateTime.Now;
        }
        
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Password { get; private set; }
        public bool Active { get; private set; }
        public string Avatar { get; private set; }
        public string Phone { get; private set; }
        public bool PendingRegisterInformation { get; private set; }
        public bool AcceptedTerm { get; private set; }
        public bool Master { get; private set; }

        public ICollection<PasswordRecoverRequest> PasswordRecoverRequests { get; private set; } = new List<PasswordRecoverRequest>();

        public User WithPassword(string password)
        {
            Password = PasswordUtils.Hash(password);
            return this;
        }

        public User WithAvatar(string avatar)
        {
            Avatar = avatar;
            return this;
        }

        public User WithName(string firstName, string lastName = null)
        {
            FirstName = firstName;
            LastName = lastName;
            return this;
        }

        public User WithMasterPermission()
        {
            Master = true;
            return this;
        }

        public bool CanAuthenticate(string passwordToCheck)
        {
            if (!CanAuthenticate(passwordToCheck, out ICollection<string> errors))
            {
                throw new DomainException(errors);
            }

            return true;
        }

        public bool CanAuthenticate(string passwordToCheck, out ICollection<string> errors)
        {
            errors = new List<string>();

            if (!CanAuthenticate(out errors))
            {
                return false;
            }

            if (PasswordUtils.Check(Password, passwordToCheck))
                return true;

            errors.Add(AppMessages.InvalidCredentials);

            return false;
        }

        public bool CanAuthenticate(out ICollection<string> erros)
        {
            erros = new List<string>();

            if (!Active)
            {
                erros.Add(AppMessages.InvalidCredentials);
                return false;
            }

            return true;
        }

        public void HandleOldPasswordRecoverRequests()
        {
            foreach (var r in PasswordRecoverRequests
                         .Where(x => x.UsedIn.HasValue && x.UsedIn < DateTime.UtcNow.AddDays(30)).ToList())
            {
                PasswordRecoverRequests.Remove(r);
            }
        }

        public PasswordRecoverRequest GeneratePasswordRecoverRequest()
        {
            HandleOldPasswordRecoverRequests();

            var passwordRecoverRequest =
                PasswordRecoverRequests
                    .FirstOrDefault(x => !x.UsedIn.HasValue);

            if (passwordRecoverRequest != null)
            {
                passwordRecoverRequest.UpdateDates();
                return passwordRecoverRequest;
            }

            passwordRecoverRequest = PasswordRecoverRequest.New(Guid.NewGuid(), Id);

            PasswordRecoverRequests.Add(passwordRecoverRequest);

            return passwordRecoverRequest;
        }

        public User ChangePassword(string currentPassword, string newPassword)
        {
            if (!PasswordUtils.Check(Password, currentPassword))
                throw new DomainException(UserMessages._Password.CurrentIncorrectError);

            Password = PasswordUtils.Hash(newPassword);
            return this;
        }

        public User ResetPasswordWithCode(string codigo, string senha)
        {
            var passwordRecoverRequest = PasswordRecoverRequests
                .FirstOrDefault(x => x.Code == codigo)?
                .UsePasswordRecoverRequest();

            if (passwordRecoverRequest == null)
                throw new DomainException(UserMessages._ResetPassword.CodeInvalidError);

            Password = PasswordUtils.Hash(senha);
            return this;
        }

        public User UserActivateWithCode(string codigo, string senha)
        {
            var activateUserRequest = PasswordRecoverRequests
                .FirstOrDefault(x => x.Code == codigo)?
                .UsePasswordRecoverRequest();

            if (activateUserRequest == null)
                throw new DomainException(UserMessages._ResetPassword.CodeInvalidError);

            Password = PasswordUtils.Hash(senha);
            return this;
        }

        public User UpdateStatus(Guid requesterUserId)
        {
            if (Id == requesterUserId)
                throw new DomainException(UserMessages.ChangeSelfStatusError);
            Active = !Active;
            return this;
        }

        public User Update(string firstName, string lastName, string avatar)
        {
            FirstName = firstName;
            LastName = lastName;
            Avatar = avatar;

            return this;
        }

        public User UpdatePersonalData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            return this;
        }

        public User Remover(Guid requesterUserId)
        {
            if (Id == requesterUserId)
                throw new DomainException(UserMessages.SelfRemoveErro);
            return this;
        }
        
        public User UpdateAcceptedTerm(bool? value)
        {
            AcceptedTerm = value ?? !AcceptedTerm;
            return this;
        }
    }
}
