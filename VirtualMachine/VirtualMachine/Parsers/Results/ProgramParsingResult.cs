using VirtualMachine.Commands;

namespace VirtualMachine.Parsers.Results;

public abstract record ProgramParsingResult
{
    private ProgramParsingResult() { }

    public sealed record Failure(string ErrorMessage) : ProgramParsingResult;
    
    public sealed record Success(List<ICommand> Commands) : ProgramParsingResult;
}