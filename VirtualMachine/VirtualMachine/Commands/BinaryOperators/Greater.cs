namespace VirtualMachine.Commands.BinaryOperators;

public class Greater : IBinaryOperation
{
    public int Apply(int left, int right)
    {
        return left > right ? 1 : 0;
    }
}
