using VirtualMachine.Contexts;

namespace VirtualMachine.Commands;

public class JumpIfNotZero(string label) : ICommand
{
    public void Execute(Context context)
    {
        var variable = context.Pop();

        if (variable != 0)
        {
            context.ProgramCounter = context.GetLabel(label);
        }
    }
}