namespace VirtualMachine.Values;

public sealed record StringValue(string Value) : IValue
{
    public string Format()
    {
        return Value;
    }
}
