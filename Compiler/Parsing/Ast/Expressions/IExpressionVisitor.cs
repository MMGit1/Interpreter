namespace Compiler.Parsing.Ast.Expressions;

public interface IExpressionVisitor
{
    void Visit(ConstantNode node);
    void Visit(StringNode node);
    void Visit(ArrayNode node);
    void Visit(ElementNode node);
    void Visit(LengthNode node);
    void Visit(SExpressionNode node);
    void Visit(VariableNode node);
    void Visit(BinaryNode node);
}
