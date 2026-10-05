using Compiler.Parsing.Ast.Expressions;

namespace Compiler.Parsing.Ast.Statements;

public sealed record IfNode(IExpressionNode Condition, IStatementNode Then, IStatementNode? Else) : IStatementNode
{
    public void Accept(IStatementVisitor visitor) => visitor.Visit(this);
}
