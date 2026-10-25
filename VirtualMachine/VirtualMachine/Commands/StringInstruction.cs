using VirtualMachine.Contexts;
using VirtualMachine.Values;

namespace VirtualMachine.Commands;

public sealed class StringInstruction(string value) : ICommand
{
    public void Execute(Context context)
    {
        context.Push(new StringValue(value));
    }
}
