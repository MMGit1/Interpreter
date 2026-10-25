using VirtualMachine.Contexts;
using VirtualMachine.Values;

namespace VirtualMachine.Commands;

public sealed class JumpIfNotZero(string label) : ICommand
{
    public void Execute(Context context)
    {
        if (context.Pop() is not IntegerValue value)
        {
            throw new InvalidOperationException("Integer value expected");
        }

        if (value.Value != 0)
        {
            context.ProgramCounter = context.GetLabel(label);
        }
    }
}
