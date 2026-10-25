namespace Compiler.Parsing.Ast.Statements;

public interface IStatementVisitor
{
    void Visit(ReadNode node);
    void Visit(WriteNode node);
    void Visit(AssignmentNode node);
    void Visit(ElementAssignmentNode node);
    void Visit(BlockNode node);
    void Visit(IfNode node);
    void Visit(WhileNode node);
    void Visit(DoWhileNode node);
    void Visit(ForNode node);
    void Visit(SkipNode node);
}
