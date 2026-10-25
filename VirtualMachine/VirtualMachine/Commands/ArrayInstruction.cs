using VirtualMachine.Contexts;
using VirtualMachine.Values;

namespace VirtualMachine.Commands;

public sealed class ArrayInstruction(int length) : ICommand
{
    public void Execute(Context context)
    {
        var elements = new IValue[length];

        for (int index = length - 1; index >= 0; index--)
        {
            elements[index] = context.Pop();
        }

        context.Push(new ArrayValue(elements));
    }
}
