using Interpreter.Exceptions;

namespace Interpreter.Ast.Expressions.BinaryExpressions;

public class Modulo(IExpression left, IExpression right)
    : BinaryExpression(left, right)
{
    protected override int Apply(int leftValue, int rightValue)
    {
        if (rightValue == 0)
            throw new DivisionByZeroException();

        return leftValue % rightValue;
    }
}