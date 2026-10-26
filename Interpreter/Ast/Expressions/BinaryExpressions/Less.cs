namespace Interpreter.Ast.Expressions.BinaryExpressions;

public sealed class Less(IExpression left, IExpression right)
    : IntegerBinaryExpression(left, right)
{
    protected override int Apply(int left, int right)
    {
        return left < right ? 1 : 0;
    }
}
