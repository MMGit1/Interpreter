using VirtualMachine.Contexts;
using VirtualMachine.Values;

namespace VirtualMachine.Commands;

public sealed class Set : ICommand
{
    public void Execute(Context context)
    {
        IValue value = context.Pop();

        if (context.Pop() is not IntegerValue index)
        {
            throw new InvalidOperationException("Integer value expected");
        }

        if (context.Pop() is not ArrayValue array)
        {
            throw new InvalidOperationException("Array value expected");
        }

        array.Set(index.Value, value);
    }
}
