namespace Compiler.Parsing.Ast.Expressions;

public sealed record LengthNode(IExpressionNode Context) : IExpressionNode
{
    public void Accept(IExpressionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
