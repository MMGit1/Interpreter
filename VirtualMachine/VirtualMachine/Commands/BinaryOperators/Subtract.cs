namespace VirtualMachine.Commands.BinaryOperators;

public class Subtract : IBinaryOperation
{
    public int Apply(int left, int right)
    {
        return left - right;
    }
}
