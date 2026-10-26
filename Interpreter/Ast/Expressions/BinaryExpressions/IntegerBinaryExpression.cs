using Interpreter.Ast.Values;
using Interpreter.Exceptions;

namespace Interpreter.Ast.Expressions.BinaryExpressions;

public abstract class IntegerBinaryExpression(IExpression left, IExpression right)
    : BinaryExpression(left, right)
{
    protected sealed override IValue Apply(IValue leftValue, IValue rightValue)
    {
        if (leftValue is not IntegerValue leftInteger)
        {
            throw new InvalidValueTypeException("Integer value expected");
        }

        if (rightValue is not IntegerValue rightInteger)
        {
            throw new InvalidValueTypeException("Integer value expected");
        }

        return new IntegerValue(Apply(leftInteger.Value, rightInteger.Value));
    }

    protected abstract int Apply(int left, int right);
}
