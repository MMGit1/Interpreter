namespace Interpreter.Exceptions;

public class DivisionByZeroException : InterpreterException
{
    public DivisionByZeroException() :  base("Division by zero") {}
}