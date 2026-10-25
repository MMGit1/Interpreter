using VirtualMachine.Contexts;
using VirtualMachine.Values;

namespace VirtualMachine.Commands;

public sealed class Element : ICommand
{
    public void Execute(Context context)
    {
        if (context.Pop() is not IntegerValue index)
        {
            throw new InvalidOperationException("Integer value expected");
        }

        if (context.Pop() is not ArrayValue array)
        {
            throw new InvalidOperationException("Array value expected");
        }

        context.Push(array.Get(index.Value));
    }
}
