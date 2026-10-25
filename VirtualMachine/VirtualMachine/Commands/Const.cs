using VirtualMachine.Contexts;
using VirtualMachine.Values;

namespace VirtualMachine.Commands;

public class Const(int value) : ICommand
{
    public void Execute(Context context)
    {
        context.Push(new IntegerValue(value));
    }
}
