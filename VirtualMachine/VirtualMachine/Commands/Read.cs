using VirtualMachine.Contexts;

namespace VirtualMachine.Commands;

public class Read : ICommand
{
    public void Execute(Context context)
    {
        context.Push(context.ReadInput());
    }
}
