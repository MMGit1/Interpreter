namespace VirtualMachine.Parsers.Results;

public abstract record TokenResult
{
    private TokenResult() { }

    public sealed record Failure(string ErrorMessage) : TokenResult;

    public sealed record Success(string[] Tokens) : TokenResult;
}