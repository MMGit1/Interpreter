using Interpreter.Ast.Values;
using Interpreter.Context;

namespace Interpreter.Ast.Expressions.BinaryExpressions;

public abstract class BinaryExpression(IExpression left, IExpression right) : IExpression
{
    private IExpression Left { get; } = left;

    private IExpression Right { get; } = right;

    public IValue Evaluate(InterpreterContext context)
    {
        IValue leftValue = Left.Evaluate(context);
        IValue rightValue = Right.Evaluate(context);

        return Apply(leftValue, rightValue);
    }

    protected abstract IValue Apply(IValue leftValue, IValue rightValue);
}
