namespace Compiler.Parsing.Ast.Expressions;

public sealed record ConstantNode(int Value) : IExpressionNode
{
    public void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
}
