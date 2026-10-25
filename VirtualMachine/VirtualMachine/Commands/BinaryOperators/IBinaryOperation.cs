using VirtualMachine.Values;

namespace VirtualMachine.Commands.BinaryOperators;

public interface IBinaryOperation
{
    IValue Apply(IValue left, IValue right);
}
