using Interpreter.Ast.Values;
using Interpreter.Context;

namespace Interpreter.Ast.Expressions;

public sealed class CharacterConstant(string value) : IExpression
{
    public IValue Evaluate(InterpreterContext context)
    {
        return new StringValue(value);
    }
}
