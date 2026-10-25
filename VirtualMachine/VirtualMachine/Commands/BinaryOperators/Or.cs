namespace VirtualMachine.Commands.BinaryOperators;

public class Or : IntegerBinaryOperation
{
    protected override int Apply(int left, int right)
    {
        return left != 0 || right != 0 ? 1 : 0;
    }
}
