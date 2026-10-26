using System.Text.Json;
using Interpreter.Ast.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Expressions;

public sealed class SExpressionLink(IExpressionParser expressionParser)
    : BaseExpressionParserLink
{
    public override ExpressionParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object ||
            !element.TryGetProperty("sexp", out JsonElement sexpElement))
        {
            return Next(element);
        }

        if (!sexpElement.TryGetProperty("name", out JsonElement nameElement) ||
            !sexpElement.TryGetProperty("arguments", out JsonElement argumentsElement) ||
            nameElement.ValueKind != JsonValueKind.String ||
            argumentsElement.ValueKind != JsonValueKind.Array)
        {
            return new ExpressionParseResult.Failure(
                "S-expression requires a name and argument array"
            );
        }

        var arguments = new List<IExpression>();

        foreach (var argumentElement in argumentsElement.EnumerateArray())
        {
            var result = expressionParser.Parse(argumentElement);

            if (result is not ExpressionParseResult.Success success)
            {
                return result;
            }

            arguments.Add(success.Expression);
        }

        return new ExpressionParseResult.Success(
            new SExpression(nameElement.GetString()!, arguments)
        );
    }
}
