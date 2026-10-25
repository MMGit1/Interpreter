namespace VirtualMachine.Commands.BinaryOperators;

public class Modulo : IntegerBinaryOperation
{
    protected override int Apply(int left, int right)
    {
        return left % right;
    }
}
