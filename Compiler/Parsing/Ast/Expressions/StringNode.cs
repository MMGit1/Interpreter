namespace Compiler.Parsing.Ast.Expressions;

public sealed record StringNode(string Value) : IExpressionNode
{
    public void Accept(IExpressionVisitor visitor)
    {
        visitor.Visit(this);
    }
}
