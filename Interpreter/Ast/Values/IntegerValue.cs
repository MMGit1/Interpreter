namespace Interpreter.Ast.Values;

public sealed record IntegerValue(int Value) : IValue
{
    public string Format() => Value.ToString();
}
