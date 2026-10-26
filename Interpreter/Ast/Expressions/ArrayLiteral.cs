using Interpreter.Ast.Values;
using Interpreter.Context;

namespace Interpreter.Ast.Expressions;

public sealed class ArrayLiteral(IReadOnlyList<IExpression> elements) : IExpression
{
    public IValue Evaluate(InterpreterContext context)
    {
        return new ArrayValue(elements.Select(element => element.Evaluate(context)).ToArray());
    }
}
