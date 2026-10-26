namespace Interpreter.Ast.Expressions.BinaryExpressions;

public sealed class And(IExpression left, IExpression right)
    : IntegerBinaryExpression(left, right)
{
    protected override int Apply(int left, int right)
    {
        return left != 0 && right != 0 ? 1 : 0;
    }
}
