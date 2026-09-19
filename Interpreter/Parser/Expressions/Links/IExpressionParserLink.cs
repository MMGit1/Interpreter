using System.Text.Json;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Expressions;

public interface IExpressionParserLink
{
    IExpressionParserLink AddNext(IExpressionParserLink nextLink);

    ExpressionParseResult Parse(JsonElement element);
}