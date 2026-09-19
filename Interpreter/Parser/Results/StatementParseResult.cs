using Interpreter.Ast.Statements;

namespace Interpreter.Parser.Results;

public abstract record StatementParseResult
{
    private StatementParseResult() { }

    public sealed record Success(IStatement Statement)
        : StatementParseResult;

    public sealed record Failure(string Message)
        : StatementParseResult;
}