namespace Compiler.Lexing;

public abstract record TokenizationResult
{
    private TokenizationResult()
    {
    }

    public sealed record Success(IReadOnlyList<Token> Tokens) : TokenizationResult;

    public sealed record Failure(string Message) : TokenizationResult;
}
