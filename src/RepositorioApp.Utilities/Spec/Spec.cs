using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RepositorioApp.Utilities.Linq;
namespace RepositorioApp.Utilities.Spec
{
    public class Spec<T> : ISpec<T> where T : class
    {
        public string ILikePattern(string value)
        {
            return $"%{value}%";
        }
        public Expression<Func<T, bool>> Predicate { get; private set; } = PredicateBuilder.True<T>();
        public ICollection<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public void AddPredicate(Expression<Func<T, bool>> predicate)
        {
            Predicate = Predicate.And(predicate);
        }

        public ISpec<T> Include(Expression<Func<T, object>> includes)
        {
            Includes.Add(includes);
            return this;
        }
    }
}
