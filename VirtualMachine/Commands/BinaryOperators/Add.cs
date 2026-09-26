namespace VirtualMachine.Commands.BinaryOperators;

public class Add : IBinaryOperation
{
    public int Apply(int left, int right)
    {
        return left + right;
    }
}
