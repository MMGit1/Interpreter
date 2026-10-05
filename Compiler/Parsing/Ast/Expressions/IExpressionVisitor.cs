namespace Compiler.Parsing.Ast.Expressions;

public interface IExpressionVisitor
{
    void Visit(ConstantNode node);
    void Visit(VariableNode node);
    void Visit(BinaryNode node);
}
