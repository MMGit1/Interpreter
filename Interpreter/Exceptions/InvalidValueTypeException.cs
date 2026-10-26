namespace Interpreter.Exceptions;

public sealed class InvalidValueTypeException(string message) : InterpreterException(message);
