namespace VirtualMachine.Commands.BinaryOperators;

public class Multiply : IBinaryOperation
{
    public int Apply(int left, int right)
    {
        return left * right;
    }
}
