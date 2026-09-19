using System.Text.Json;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Statements.Links;

public abstract class BaseStatementParserLink : IStatementParserLink
{
    private IStatementParserLink? _nextLink;

    public IStatementParserLink AddNext(IStatementParserLink nextLink)
    {
        if (_nextLink is null)
        {
            _nextLink = nextLink;
        }
        else
        {
            _nextLink.AddNext(nextLink);
        }

        return this;
    }

    public abstract StatementParseResult Parse(JsonElement element);

    protected StatementParseResult Next(JsonElement element)
    {
        return _nextLink is null
            ? new StatementParseResult.Failure("Unknown statement")
            : _nextLink.Parse(element);
    }
}