namespace Compiler.Parsing.Ast.Expressions;

public sealed record SExpressionNode(
    string Name,
    IReadOnlyList<IExpressionNode> Arguments) : IExpressionNode
{
    public void Accept(IExpressionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
