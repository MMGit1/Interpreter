using Interpreter.Exceptions;

namespace Interpreter.Context;

public class InterpreterContext(
    TextReader input,
    TextWriter output)
{
    private readonly Dictionary<string, int> _variables = new();
    private readonly Queue<string> _inputBuffer = new();

    public int GetVariable(string name)
    {
        if (!_variables.TryGetValue(name, out var value))
            throw new UndefinedVariableException(name);

        return value;
    }

    public void SetVariable(string name, int value)
    {
        _variables[name] = value;
    }

    public int ReadInput()
    {
        while (_inputBuffer.Count == 0)
        {
            string? line = input.ReadLine();

            if (line is null)
                throw new NoInputException();

            foreach (var token in line.Split(
                         ' ',
                         StringSplitOptions.RemoveEmptyEntries))
            {
                _inputBuffer.Enqueue(token);
            }
        }

        return int.Parse(_inputBuffer.Dequeue());
    }

    public void WriteOutput(int value)
    {
        output.WriteLine(value);
    }
}