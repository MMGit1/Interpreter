namespace Compiler.Parsing.Ast.Expressions;

public sealed record ArrayNode(IReadOnlyList<IExpressionNode> Elements) : IExpressionNode
{
    public void Accept(IExpressionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
