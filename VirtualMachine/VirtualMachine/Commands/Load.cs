using VirtualMachine.Contexts;

namespace VirtualMachine.Commands;

public class Load(string name) : ICommand
{
    public void Execute(Context context)
    {
        var variable = context.GetVariable(name);
        
        context.Push(variable);
    }
}