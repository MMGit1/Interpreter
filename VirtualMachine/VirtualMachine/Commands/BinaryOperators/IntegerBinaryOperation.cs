using VirtualMachine.Values;

namespace VirtualMachine.Commands.BinaryOperators;

public abstract class IntegerBinaryOperation : IBinaryOperation
{
    public IValue Apply(IValue left, IValue right)
    {
        if (left is not IntegerValue leftInteger)
        {
            throw new InvalidOperationException("Integer value expected");
        }

        if (right is not IntegerValue rightInteger)
        {
            throw new InvalidOperationException("Integer value expected");
        }

        return new IntegerValue(Apply(leftInteger.Value, rightInteger.Value));
    }

    protected abstract int Apply(int left, int right);
}
