using Interpreter.Ast.Values;
using Interpreter.Context;

namespace Interpreter.Ast.Expressions;

public sealed class SExpression(string name, IReadOnlyList<IExpression> arguments)
    : IExpression
{
    public IValue Evaluate(InterpreterContext context)
    {
        var values = arguments
            .Select(argument => argument.Evaluate(context))
            .ToArray();

        return new SExpressionValue(name, values);
    }
}
