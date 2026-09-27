using VirtualMachine.Contexts;

namespace VirtualMachine.Commands;

public interface ICommand
{
    void Execute(Context context);
}