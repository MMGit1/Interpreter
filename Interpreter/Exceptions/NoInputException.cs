namespace Interpreter.Exceptions;

public class NoInputException : InterpreterException
{
    public NoInputException()
        : base("No input available")
    {
    }
}