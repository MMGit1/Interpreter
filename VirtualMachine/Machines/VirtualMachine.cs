using VirtualMachine.Commands;
using VirtualMachine.Contexts;

namespace VirtualMachine.Machines;

public class VirtualMachine
{
    public void Execute(IReadOnlyList<ICommand> commands, Context context)
    {
        RegisterLabels(commands, context);

        while (context.ProgramCounter < commands.Count)
        {
            var command = commands[context.ProgramCounter];
            context.ProgramCounter++;
            command.Execute(context);
        }
    }

    private static void RegisterLabels(
        IReadOnlyList<ICommand> commands,
        Context context)
    {
        for (var index = 0; index < commands.Count; index++)
        {
            if (commands[index] is Label label)
            {
                context.SetLabel(label.Name, index);
            }
        }
    }
}
