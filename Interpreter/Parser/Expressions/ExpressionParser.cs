using System.Text.Json;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Expressions;

public class ExpressionParser : IExpressionParser
{
    private readonly IExpressionParserLink _root;

    public ExpressionParser(IExpressionChainFactory chainFactory)
    {
        _root = chainFactory.Create(this);
    }

    public ExpressionParseResult Parse(JsonElement element)
    {
        return _root.Parse(element);
    }
}