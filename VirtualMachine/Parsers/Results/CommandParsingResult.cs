using VirtualMachine.Commands;

namespace VirtualMachine.Parsers.Results;

public abstract record CommandParsingResult
{
    private CommandParsingResult() { }
    
    public sealed record Failure(string ErrorMessage) : CommandParsingResult;

    public sealed record Success(ICommand Command) : CommandParsingResult;
}