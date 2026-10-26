namespace Interpreter.Ast.Values;

public sealed class SExpressionValue(
    string name,
    IReadOnlyList<IValue> arguments) : IValue
{
    private readonly IReadOnlyList<IValue> _arguments = arguments;

    public string Format()
    {
        if (_arguments.Count == 0)
        {
            return name;
        }

        return $"{name}({string.Join(", ", _arguments.Select(argument => argument.Format()))})";
    }
}
