using Interpreter.Ast.Values;
using Interpreter.Exceptions;

namespace Interpreter.Ast.Expressions.BinaryExpressions;

public sealed class Cons(IExpression left, IExpression right)
    : BinaryExpression(left, right)
{
    protected override IValue Apply(IValue leftValue, IValue rightValue)
    {
        if (rightValue is not ArrayValue array)
        {
            throw new InvalidValueTypeException("Array value expected");
        }

        return array.Prepend(leftValue);
    }
}
