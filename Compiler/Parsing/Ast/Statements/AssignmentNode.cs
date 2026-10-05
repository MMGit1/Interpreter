using Compiler.Parsing.Ast.Expressions;
using Compiler.Parsing.Operators;

namespace Compiler.Parsing.Ast.Statements;

public sealed record AssignmentNode(string Name, IAssignmentOperator Operator, IExpressionNode Expression) : IStatementNode
{
    public void Accept(IStatementVisitor visitor) => visitor.Visit(this);
}
