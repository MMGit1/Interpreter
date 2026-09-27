using VirtualMachine.Contexts;

namespace VirtualMachine.Commands;

public class Label(string name) : ICommand
{
    public string Name { get; } = name;

    public void Execute(Context context)
    {
    }
}
