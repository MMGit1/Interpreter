namespace Interpreter.Ast.Expressions.BinaryExpressions;

public class NotEqual(IExpression left, IExpression right)
    : BinaryExpression(left, right)
{
    protected override int Apply(int leftValue, int rightValue)
    {
        return leftValue != rightValue ? 1 : 0;
    }
}