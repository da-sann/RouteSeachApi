using System;
using System.Linq.Expressions;

namespace RouteSeachApi.Helpers.Visitors {
    internal class SwapVisitor : ExpressionVisitor {
        public SwapVisitor(Expression from, Expression to) {
            _from = from;
            _to = to;
        }
        private readonly Expression _from;
        private readonly Expression _to;
        public override Expression Visit(Expression node) {
            return node == _from ? _to : base.Visit(node);
        }
    }
}
