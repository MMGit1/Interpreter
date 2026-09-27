namespace VirtualMachine.Commands.BinaryOperators;

public class LessOrEqual : IBinaryOperation
{
    public int Apply(int left, int right)
    {
        return left <= right ? 1 : 0;
    }
}
