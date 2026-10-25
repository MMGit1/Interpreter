using VirtualMachine.Values;

namespace VirtualMachine.Commands.BinaryOperators;

public sealed class Cons : IBinaryOperation
{
    public IValue Apply(IValue left, IValue right)
    {
        if (right is not ArrayValue array)
        {
            throw new InvalidOperationException("Array value expected");
        }

        return array.Prepend(left);
    }
}
