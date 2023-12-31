using System.Collections.Generic;
using System.Linq;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Domain.ViewsModels;
namespace RepositorioApp.Domain.Projections
{
    public static class UserProjection
    {
        public static IQueryable<UserVm> ToVm(this IQueryable<User> query)
        {
            return query.Select(user => new UserVm
            {
                Id = user.Id,
                Email = user.Email,
                BirthDate = user.BirthDate,

                Active = user.Active,
                Avatar = user.Avatar,
                Master = user.Master,
                
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                FullName = user.FirstName + " " + user.LastName,
                
                PendingRegisterInformation = user.PendingRegisterInformation,
                AcceptedTerm = user.AcceptedTerm,
                
                CreatedAt = user.CreatedAt,
                
                University = user.University != null ? user.University.ToVm() : null,
                EducationalRole = user.EducationalRole != null ? user.EducationalRole.ToVm() : null
            });
        }

        public static IEnumerable<UserVm> ToVm(this IEnumerable<User> query)
        {
            return query.AsQueryable().ToVm();
        }

        public static UserVm ToVm(this User user)
        {
            return new[]
            {
                user
            }.AsQueryable().ToVm().FirstOrDefault();
        }
    }
}
