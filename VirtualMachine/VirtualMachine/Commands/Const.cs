using VirtualMachine.Contexts;

namespace VirtualMachine.Commands;

public class Const(int value) : ICommand
{
    public void Execute(Context context)
    {
        context.Push(value);
    }
}