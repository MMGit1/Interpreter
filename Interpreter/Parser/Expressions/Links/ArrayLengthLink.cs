using System.Text.Json;
using Interpreter.Ast.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Expressions;

public sealed class ArrayLengthLink(IExpressionParser expressionParser) : BaseExpressionParserLink
{
    public override ExpressionParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object || !element.TryGetProperty("length", out var array))
        {
            return Next(element);
        }

        var result = expressionParser.Parse(array);

        return result is ExpressionParseResult.Success success
            ? new ExpressionParseResult.Success(new ArrayLength(success.Expression))
            : result;
    }
}
