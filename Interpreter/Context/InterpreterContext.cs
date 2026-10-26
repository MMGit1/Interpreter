using Interpreter.Ast.Values;
using Interpreter.Exceptions;

namespace Interpreter.Context;

public class InterpreterContext(
    TextReader input,
    TextWriter output)
{
    private readonly Dictionary<string, IValue> _variables = new();
    private readonly Queue<string> _inputBuffer = new();

    public IValue GetVariable(string name)
    {
        if (!_variables.TryGetValue(name, out var value))
        {
            throw new UndefinedVariableException(name);
        }

        return value;
    }

    public void SetVariable(string name, IValue value)
    {
        _variables[name] = value;
    }

    public IValue ReadInput()
    {
        while (_inputBuffer.Count == 0)
        {
            string? line = input.ReadLine();

            if (line is null)
            {
                throw new NoInputException();
            }

            foreach (var token in line.Split(
                         ' ',
                         StringSplitOptions.RemoveEmptyEntries))
            {
                _inputBuffer.Enqueue(token);
            }
        }

        return new IntegerValue(int.Parse(_inputBuffer.Dequeue()));
    }

    public void WriteOutput(IValue value)
    {
        output.WriteLine(value.Format());
    }
}
