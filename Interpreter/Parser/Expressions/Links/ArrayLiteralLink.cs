using System.Text.Json;
using Interpreter.Ast.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Expressions;

public sealed class ArrayLiteralLink(IExpressionParser expressionParser) : BaseExpressionParserLink
{
    public override ExpressionParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object || !element.TryGetProperty("array", out var values))
        {
            return Next(element);
        }

        if (values.ValueKind != JsonValueKind.Array)
        {
            return new ExpressionParseResult.Failure("Array elements must be an array");
        }

        var expressions = new List<IExpression>();

        foreach (var value in values.EnumerateArray())
        {
            var result = expressionParser.Parse(value);

            if (result is not ExpressionParseResult.Success success)
            {
                return result;
            }

            expressions.Add(success.Expression);
        }

        return new ExpressionParseResult.Success(new ArrayLiteral(expressions));
    }
}
