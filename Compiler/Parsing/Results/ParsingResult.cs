using Compiler.Parsing.Ast;

namespace Compiler.Parsing.Results;

public abstract record ParsingResult
{
    private ParsingResult()
    {
    }

    public sealed record Success(ProgramNode Program) : ParsingResult;

    public sealed record Failure(string ErrorMessage) : ParsingResult;
}
