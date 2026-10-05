namespace Compiler.Lexing;

public sealed record Token(string Text, int Line, int Column);
