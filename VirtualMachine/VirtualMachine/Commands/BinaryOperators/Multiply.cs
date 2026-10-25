namespace VirtualMachine.Commands.BinaryOperators;

public class Multiply : IntegerBinaryOperation
{
    protected override int Apply(int left, int right)
    {
        return left * right;
    }
}
