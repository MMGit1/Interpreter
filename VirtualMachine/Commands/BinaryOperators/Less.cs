namespace VirtualMachine.Commands.BinaryOperators;

public class Less : IBinaryOperation
{
    public int Apply(int left, int right)
    {
        return left < right ? 1 : 0;
    }
}
