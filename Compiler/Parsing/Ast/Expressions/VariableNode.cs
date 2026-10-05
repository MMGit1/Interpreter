namespace Compiler.Parsing.Ast.Expressions;

public sealed record VariableNode(string Name) : IExpressionNode
{
    public void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
}
