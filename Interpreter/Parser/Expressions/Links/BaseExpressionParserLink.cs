using System.Text.Json;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Expressions;

public abstract class BaseExpressionParserLink : IExpressionParserLink
{
    private IExpressionParserLink? _nextLink;
    
    public IExpressionParserLink AddNext(IExpressionParserLink nextLink)
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

    public abstract ExpressionParseResult Parse(JsonElement element);

    protected ExpressionParseResult Next(JsonElement element)
    {
        return _nextLink is null
            ? new ExpressionParseResult.Failure("Unknown expression")
            : _nextLink.Parse(element);
    }
}