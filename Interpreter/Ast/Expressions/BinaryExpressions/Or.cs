namespace Interpreter.Ast.Expressions.BinaryExpressions;

public class Or(IExpression left, IExpression right)
    : BinaryExpression(left, right)
{
    protected override int Apply(int leftValue, int rightValue)
    {
        return leftValue != 0 || rightValue != 0 ? 1 : 0;
    }
}