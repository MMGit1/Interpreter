using Compiler.Parsing.Ast.Expressions;

namespace Compiler.Parsing.Ast.Statements;

public sealed record ForNode(AssignmentNode Initialization, IExpressionNode Condition, AssignmentNode Update, IStatementNode Body) : IStatementNode
{
    public void Accept(IStatementVisitor visitor) => visitor.Visit(this);
}
