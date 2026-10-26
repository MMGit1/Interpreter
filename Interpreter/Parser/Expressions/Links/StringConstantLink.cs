using System.Text.Json;
using Interpreter.Ast.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Expressions;

public sealed class StringConstantLink : BaseExpressionParserLink
{
    public override ExpressionParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object || !element.TryGetProperty("string", out var value))
        {
            return Next(element);
        }

        return value.ValueKind == JsonValueKind.String
            ? new ExpressionParseResult.Success(new StringConstant(value.GetString()!))
            : new ExpressionParseResult.Failure("Invalid string value");
    }
}
