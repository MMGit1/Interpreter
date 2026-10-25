using VirtualMachine.Values;

namespace VirtualMachine.Contexts;

public class Context(
    TextReader input,
    TextWriter output)
{
    private readonly Stack<IValue> _stack = new Stack<IValue>();

    private readonly Dictionary<string, IValue> _variables = new Dictionary<string, IValue>();
    
    private readonly Dictionary<string, int> _labels = new Dictionary<string, int>();

    public int ProgramCounter { get; set; } = 0;

    public void Push(IValue value)
    {
        _stack.Push(value);
    }

    public IValue Pop()
    {
        return _stack.Pop();
    }

    public IValue GetVariable(string name)
    {
        if (!_variables.TryGetValue(name, out var value))
        {
            throw new KeyNotFoundException("No such variable");
        }

        return value;
    }

    public void SetVariable(string name, IValue value)
    {
        _variables[name] = value;
    }

    public int GetLabel(string name)
    {
        if (!_labels.TryGetValue(name, out var value))
        {
            throw new KeyNotFoundException("no such label");
        }
        
        return value;
    }

    public void SetLabel(string name, int value)
    {
        _labels[name] = value;
    }

    public IValue ReadInput()
    {
        var line = input.ReadLine();

        if (line is null)
        {
            throw new InvalidOperationException("No input available");
        }

        if (!int.TryParse(line, out var result))
        {
            throw new FormatException($"'{line}' is not an integer");
        }

        return new IntegerValue(result);
    }

    public void Write(IValue value)
    {
        output.WriteLine(value.Format());
    }
}
