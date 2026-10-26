using Interpreter.Ast.Values;
using Interpreter.Context;
using Interpreter.Exceptions;

namespace Interpreter.Ast.Expressions;

public sealed class ArrayElementAccess(IExpression array, IExpression index) : IExpression
{
    public IValue Evaluate(InterpreterContext context)
    {
        return GetArray(context).Get(GetIndex(context));
    }

    public void SetValue(InterpreterContext context, IValue value)
    {
        GetArray(context).Set(GetIndex(context), value);
    }

    private ArrayValue GetArray(InterpreterContext context)
    {
        if (array.Evaluate(context) is not ArrayValue arrayValue)
        {
            throw new InvalidValueTypeException("Array value expected");
        }

        return arrayValue;
    }

    private int GetIndex(InterpreterContext context)
    {
        if (index.Evaluate(context) is not IntegerValue indexValue)
        {
            throw new InvalidValueTypeException("Integer value expected");
        }

        return indexValue.Value;
    }
}
