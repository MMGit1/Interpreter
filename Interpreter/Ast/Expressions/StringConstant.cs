using Interpreter.Ast.Values;
using Interpreter.Context;

namespace Interpreter.Ast.Expressions;

public sealed class StringConstant(string value) : IExpression
{
    public IValue Evaluate(InterpreterContext context)
    {
        return new StringValue(value);
    }
}
