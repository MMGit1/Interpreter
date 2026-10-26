namespace Interpreter.Ast.Expressions.BinaryExpressions;

public sealed class Subtract(IExpression left, IExpression right)
    : IntegerBinaryExpression(left, right)
{
    protected override int Apply(int left, int right)
    {
        return left - right;
    }
}
