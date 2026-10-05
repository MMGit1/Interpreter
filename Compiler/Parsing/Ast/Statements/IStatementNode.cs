namespace Compiler.Parsing.Ast.Statements;

public interface IStatementNode
{
    void Accept(IStatementVisitor visitor);
}
