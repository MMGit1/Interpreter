namespace VirtualMachine.Commands.BinaryOperators;

public class Add : IntegerBinaryOperation
{
    protected override int Apply(int left, int right)
    {
        return left + right;
    }
}
