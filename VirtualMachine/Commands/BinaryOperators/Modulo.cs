namespace VirtualMachine.Commands.BinaryOperators;

public class Modulo : IBinaryOperation
{
    public int Apply(int left, int right)
    {
        return left % right;
    }
}
