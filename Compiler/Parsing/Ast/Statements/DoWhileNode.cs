using Compiler.Parsing.Ast.Expressions;

namespace Compiler.Parsing.Ast.Statements;

public sealed record DoWhileNode(IStatementNode Body, IExpressionNode Condition) : IStatementNode
{
    public void Accept(IStatementVisitor visitor) => visitor.Visit(this);
}
