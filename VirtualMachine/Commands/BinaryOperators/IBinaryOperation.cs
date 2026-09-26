namespace VirtualMachine.Commands.BinaryOperators;

public interface IBinaryOperation
{
    int Apply(int left, int right);
}
