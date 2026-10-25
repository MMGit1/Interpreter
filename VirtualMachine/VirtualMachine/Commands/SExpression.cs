using VirtualMachine.Contexts;
using VirtualMachine.Values;

namespace VirtualMachine.Commands;

public sealed class SExpression(string name, int argumentCount) : ICommand
{
    public void Execute(Context context)
    {
        var arguments = new IValue[argumentCount];

        for (int index = argumentCount - 1; index >= 0; index--)
        {
            arguments[index] = context.Pop();
        }

        context.Push(new SExpressionValue(name, arguments));
    }
}
