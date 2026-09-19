namespace Interpreter.Exceptions;

public class UndefinedVariableException(string variableName)
    : InterpreterException($"Undefined variable: {variableName}")
{
    public string VariableName { get; } = variableName;
}