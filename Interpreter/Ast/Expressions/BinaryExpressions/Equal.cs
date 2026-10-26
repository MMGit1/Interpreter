using Interpreter.Ast.Values;

namespace Interpreter.Ast.Expressions.BinaryExpressions;

public class Equal(IExpression left, IExpression right)
    : BinaryExpression(left, right)
{
    protected override IValue Apply(IValue leftValue, IValue rightValue)
    {
        return new IntegerValue(Equals(leftValue, rightValue) ? 1 : 0);
    }
}
