namespace Compiler.Parsing.Ast.Expressions;

public sealed record ElementNode(IExpressionNode Context, IExpressionNode Index) : IExpressionNode
{
    public void Accept(IExpressionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
