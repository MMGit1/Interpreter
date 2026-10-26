using Interpreter.Exceptions;

namespace Interpreter.Ast.Expressions.BinaryExpressions;

public sealed class Modulo(IExpression left, IExpression right)
    : IntegerBinaryExpression(left, right)
{
    protected override int Apply(int left, int right)
    {
        if (right == 0)
        {
            throw new DivisionByZeroException();
        }

        return left % right;
    }
}
