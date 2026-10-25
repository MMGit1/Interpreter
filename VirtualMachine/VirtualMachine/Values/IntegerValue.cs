namespace VirtualMachine.Values;

public sealed record IntegerValue(int Value) : IValue
{
    public string Format()
    {
        return Value.ToString();
    }
}
