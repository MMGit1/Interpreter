namespace VirtualMachine.Commands.BinaryOperators;

public class Divide : IBinaryOperation
{
    public int Apply(int left, int right)
    {
        return left / right;
    }
}
