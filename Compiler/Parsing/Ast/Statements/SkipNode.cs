namespace Compiler.Parsing.Ast.Statements;

public sealed record SkipNode : IStatementNode
{
    public void Accept(IStatementVisitor visitor) => visitor.Visit(this);
}
