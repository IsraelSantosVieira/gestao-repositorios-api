using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace RepositorioApp.Utilities.Linq
{
    public class IncludeHelper<T>
    {

        public IncludeHelper()
        {
            Includes = new List<string>();
        }
        public ICollection<string> Includes { get; }

        public IncludeHelper<T> Include(Expression<Func<T, object>> exp)
        {
            Includes.Add(Get(exp));
            return this;
        }

        private static string Get(Expression<Func<T, object>> exp)
        {
            TryParsePath(exp.Body, out var include);
            return include;
        }

        private static bool TryParsePath(Expression expression, out string path)
        {
            path = null;
            var withoutConvert = RemoveConvert(expression);// Removes boxing

            if (withoutConvert is MemberExpression memberExpression)
            {
                var thisPart = memberExpression.Member.Name;
                if (!TryParsePath(memberExpression.Expression, out var parentPart))
                {
                    return false;
                }
                path = parentPart == null ? thisPart : parentPart + "." + thisPart;
            }
            else if (withoutConvert is MethodCallExpression callExpression)
            {
                if (callExpression.Method.Name == "Select"
                    && callExpression.Arguments.Count == 2)
                {
                    string parentPart;
                    if (!TryParsePath(callExpression.Arguments[0], out parentPart))
                    {
                        return false;
                    }
                    if (parentPart != null)
                    {
                        var subExpression = callExpression.Arguments[1] as LambdaExpression;
                        if (subExpression != null)
                        {
                            if (!TryParsePath(subExpression.Body, out var thisPart))
                            {
                                return false;
                            }
                            if (thisPart != null)
                            {
                                path = parentPart + "." + thisPart;
                                return true;
                            }
                        }
                    }
                }
                return false;
            }

            return true;
        }

        private static Expression RemoveConvert(Expression expression)
        {
            while (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked)
            {
                expression = ((UnaryExpression)expression).Operand;
            }

            return expression;
        }
    }
}
