using Compiler.Parsing.Ast.Expressions;

namespace Compiler.Parsing.Ast.Statements;

public sealed record WriteNode(IExpressionNode Expression) : IStatementNode
{
    public void Accept(IStatementVisitor visitor) => visitor.Visit(this);
}
