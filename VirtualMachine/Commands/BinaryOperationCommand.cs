using VirtualMachine.Commands.BinaryOperators;
using VirtualMachine.Contexts;

namespace VirtualMachine.Commands;

public class BinaryOperationCommand(IBinaryOperation operation) : ICommand
{
    public void Execute(Context context)
    {
        var right = context.Pop();
        var left = context.Pop();
        context.Push(operation.Apply(left, right));
    }
}
