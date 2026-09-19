using System.Text.Json;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Expressions;

public interface IExpressionParser
{
    ExpressionParseResult Parse(JsonElement element);
}