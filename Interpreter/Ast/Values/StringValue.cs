namespace Interpreter.Ast.Values;

public sealed record StringValue(string Value) : IValue
{
    public string Format() => Value;
}
