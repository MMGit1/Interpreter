namespace Compiler.Parsing.Ast.Statements;

public sealed record BlockNode(IReadOnlyList<IStatementNode> Statements) : IStatementNode
{
    public void Accept(IStatementVisitor visitor) => visitor.Visit(this);
}
