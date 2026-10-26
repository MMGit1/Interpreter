using Interpreter.Ast.Values;
using Interpreter.Context;
using Interpreter.Exceptions;

namespace Interpreter.Ast.Expressions;

public sealed class ArrayLength(IExpression array) : IExpression
{
    public IValue Evaluate(InterpreterContext context)
    {
        if (array.Evaluate(context) is not ArrayValue arrayValue)
        {
            throw new InvalidValueTypeException("Array value expected");
        }

        return new IntegerValue(arrayValue.Length);
    }
}
