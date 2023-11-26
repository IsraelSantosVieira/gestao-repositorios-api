using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace RepositorioApp.Utilities.Spec
{
    public interface ISpec<T> where T : class
    {
        Expression<Func<T, bool>> Predicate { get; }
        ICollection<Expression<Func<T, object>>> Includes { get; }
        string ILikePattern(string value);
        void AddPredicate(Expression<Func<T, bool>> predicate);
        ISpec<T> Include(Expression<Func<T, object>> includes);
    }
}
