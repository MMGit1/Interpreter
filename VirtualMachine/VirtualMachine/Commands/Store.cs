using VirtualMachine.Contexts;

namespace VirtualMachine.Commands;

public class Store(string name) : ICommand
{
    public void Execute(Context context)
    {
        var variable = context.Pop();
        
        context.SetVariable(name, variable);
    }
}