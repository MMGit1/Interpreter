using Compiler.Parsing.Operators;

namespace Compiler.Parsing.Ast.Expressions;

public sealed record BinaryNode(IExpressionNode Left, IBinaryOperator Operator, IExpressionNode Right) : IExpressionNode
{
    public void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
}
