using VirtualMachine.Contexts;

namespace VirtualMachine.Commands;

public class Jump(string label) : ICommand
{
    public void Execute(Context context)
    {
        context.ProgramCounter = context.GetLabel(label);
    }
}
