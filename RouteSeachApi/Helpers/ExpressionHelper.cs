using System;
using System.Linq.Expressions;
using RouteSeachApi.Helpers.Visitors;

namespace RouteSeachApi.Helpers {
    public static class ExpressionHelper {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2) {
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    new SwapVisitor(expr1.Parameters[0], expr2.Parameters[0]).Visit(expr1.Body),
                    expr2.Body
                ), expr2.Parameters);
        }
    }
}
