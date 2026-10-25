using VirtualMachine.Contexts;
using VirtualMachine.Values;

namespace VirtualMachine.Commands;

public sealed class Length : ICommand
{
    public void Execute(Context context)
    {
        if (context.Pop() is not ArrayValue array)
        {
            throw new InvalidOperationException("Array value expected");
        }

        context.Push(new IntegerValue(array.Length));
    }
}
