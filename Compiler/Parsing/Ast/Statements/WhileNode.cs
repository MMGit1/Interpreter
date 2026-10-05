using Compiler.Parsing.Ast.Expressions;

namespace Compiler.Parsing.Ast.Statements;

public sealed record WhileNode(IExpressionNode Condition, IStatementNode Body) : IStatementNode
{
    public void Accept(IStatementVisitor visitor) => visitor.Visit(this);
}
