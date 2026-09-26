using VirtualMachine.Contexts;

namespace VirtualMachine.Commands;

public class Write : ICommand
{
    public void Execute(Context context)
    {
        var variable = context.Pop();
        
        context.Write(variable);
    }
}