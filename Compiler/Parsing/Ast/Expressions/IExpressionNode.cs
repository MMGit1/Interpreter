namespace Compiler.Parsing.Ast.Expressions;

public interface IExpressionNode
{
    void Accept(IExpressionVisitor visitor);
}
