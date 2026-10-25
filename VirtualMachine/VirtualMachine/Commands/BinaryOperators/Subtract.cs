namespace VirtualMachine.Commands.BinaryOperators;

public class Subtract : IntegerBinaryOperation
{
    protected override int Apply(int left, int right)
    {
        return left - right;
    }
}
