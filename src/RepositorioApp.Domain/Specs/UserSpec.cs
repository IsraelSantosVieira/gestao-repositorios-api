using System;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Utilities.Spec;
namespace RepositorioApp.Domain.Specs
{
    public class UserSpec : Spec<User>
    {
        public UserSpec ById(Guid id)
        {
            AddPredicate(x => x.Id == id);
            return this;
        }

        public UserSpec ByName(string firstName, string lastName = null)
        {
            var name = $"{firstName} {lastName}".Trim();
            if (!string.IsNullOrWhiteSpace(name))
                AddPredicate(x => (x.FirstName + " " + x.LastName).ToLower().Contains(name.ToLower()));
            return this;
        }

        public UserSpec ByEmail(string email)
        {
            AddPredicate(x => x.Email.ToLower() == email.ToLower());
            return this;
        }
    }
}
