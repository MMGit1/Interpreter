namespace VirtualMachine.Contexts;

public class Context(
    TextReader input,
    TextWriter output)
{
    private readonly Stack<int> _stack = new Stack<int>();

    private readonly Dictionary<string, int> _variables = new Dictionary<string, int>();
    
    private readonly Dictionary<string, int> _labels = new Dictionary<string, int>();

    public int ProgramCounter { get; set; } = 0;

    public void Push(int value)
    {
        _stack.Push(value);
    }

    public int Pop()
    {
        return _stack.Pop();
    }

    public int GetVariable(string name)
    {
        if (!_variables.TryGetValue(name, out var value))
        {
            throw new KeyNotFoundException("No such variable");
        }

        return value;
    }

    public void SetVariable(string name, int value)
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

    public int ReadInput()
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

        return result;
    }
    
    public void Write(int value)
    {
        output.WriteLine(value);
    }
}