namespace VirtualMachine.Commands.BinaryOperators;

public class LessOrEqual : IntegerBinaryOperation
{
    protected override int Apply(int left, int right)
    {
        return left <= right ? 1 : 0;
    }
}
