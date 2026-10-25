namespace VirtualMachine.Commands.BinaryOperators;

using VirtualMachine.Values;

public class NotEqual : IBinaryOperation
{
    public IValue Apply(IValue left, IValue right)
    {
        return new IntegerValue(!Equals(left, right) ? 1 : 0);
    }
}
