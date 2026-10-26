using System.Text.Json;
using Interpreter.Ast.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Expressions;

public sealed class ArrayElementAccessLink(IExpressionParser expressionParser) : BaseExpressionParserLink
{
    public override ExpressionParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object ||
            !TryGetAccessElement(element, out JsonElement access))
        {
            return Next(element);
        }

        if (!TryGetContextElement(access, out JsonElement array) ||
            !access.TryGetProperty("index", out JsonElement index))
        {
            return new ExpressionParseResult.Failure("Array access requires array and index");
        }

        var arrayResult = expressionParser.Parse(array);

        if (arrayResult is not ExpressionParseResult.Success arraySuccess)
        {
            return arrayResult;
        }

        var indexResult = expressionParser.Parse(index);

        if (indexResult is not ExpressionParseResult.Success indexSuccess)
        {
            return indexResult;
        }

        return new ExpressionParseResult.Success(
            new ArrayElementAccess(
                arraySuccess.Expression,
                indexSuccess.Expression
            )
        );
    }

    private static bool TryGetAccessElement(
        JsonElement element,
        out JsonElement access)
    {
        return element.TryGetProperty("elem", out access) ||
               element.TryGetProperty("index", out access);
    }

    private static bool TryGetContextElement(
        JsonElement access,
        out JsonElement array)
    {
        return access.TryGetProperty("context", out array) ||
               access.TryGetProperty("array", out array);
    }
}
