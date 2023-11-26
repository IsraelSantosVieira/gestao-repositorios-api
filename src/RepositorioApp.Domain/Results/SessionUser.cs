using System;
using System.Collections.Generic;
using System.Security.Claims;
using Newtonsoft.Json;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.Domain.Entities;
namespace RepositorioApp.Domain.Results
{
    public class SessionUser
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Avatar { get; set; }
        
        public string Phone { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();

        public bool PendingRegisterInformation { get; private set; }

        public bool? AcceptedTerm { get; set; }
        
        public bool Master { get; set; }

        [JsonIgnore]
        public IEnumerable<Claim> Claims
        {
            get
            {
                var claims = new List<Claim>
                {
                    new Claim(CustomClaims.Id, Id.ToString()),
                    new Claim(CustomClaims.Sub, Id.ToString()),
                    new Claim(CustomClaims.Email, Email),
                    new Claim(CustomClaims.Phone, Phone),
                    new Claim(CustomClaims.FullName, FullName),
                    new Claim(CustomClaims.Master, Master.ToString())
                };

                if (!string.IsNullOrWhiteSpace(Avatar))
                {
                    claims.Add(new Claim(CustomClaims.Avatar, Avatar));
                }

                return claims;
            }
        }

        [JsonIgnore]
        public ClaimsIdentity Identity => new ClaimsIdentity(Claims);
        
        public static SessionUser GetSessionUser(User userApp)
        {
            return new SessionUser
            {
                PendingRegisterInformation = userApp.PendingRegisterInformation,
                Avatar = userApp.Avatar,
                Email = userApp.Email,
                FirstName = userApp.FirstName,
                Id = userApp.Id,
                LastName = userApp.LastName,
                Phone = userApp.Phone,
                AcceptedTerm = userApp.AcceptedTerm,
                Master = userApp.Master
            };
        }
    }
}
