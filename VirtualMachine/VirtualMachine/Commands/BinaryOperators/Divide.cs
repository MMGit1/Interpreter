namespace VirtualMachine.Commands.BinaryOperators;

public class Divide : IntegerBinaryOperation
{
    protected override int Apply(int left, int right)
    {
        return left / right;
    }
}
