using Interpreter.Context;

namespace Interpreter.Ast.Expressions.BinaryExpressions;

public abstract class BinaryExpression(IExpression left, IExpression right) : IExpression
{
    private IExpression Left { get; } = left;

    private IExpression Right { get; } = right;

    public int Evaluate(InterpreterContext context)
    {
        var leftValue = Left.Evaluate(context);
        var rightValue = Right.Evaluate(context);
        
        return Apply(leftValue, rightValue);
    }
    
    protected abstract int Apply(int leftValue, int rightValue);
}