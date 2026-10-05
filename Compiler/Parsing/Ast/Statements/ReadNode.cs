namespace Compiler.Parsing.Ast.Statements;

public sealed record ReadNode(string Name) : IStatementNode
{
    public void Accept(IStatementVisitor visitor) => visitor.Visit(this);
}
