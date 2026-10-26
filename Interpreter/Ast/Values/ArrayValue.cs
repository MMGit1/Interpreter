namespace Interpreter.Ast.Values;

public sealed class ArrayValue(IReadOnlyList<IValue> elements) : IValue
{
    private readonly List<IValue> _elements = elements.ToList();

    public int Length => _elements.Count;

    public IValue Get(int index)
    {
        return _elements[index];
    }

    public void Set(int index, IValue value)
    {
        _elements[index] = value;
    }

    public ArrayValue Prepend(IValue value)
    {
        return new ArrayValue([value, .. _elements]);
    }

    public string Format()
    {
        return $"[{string.Join("; ", _elements.Select(element => element.Format()))}]";
    }
}
