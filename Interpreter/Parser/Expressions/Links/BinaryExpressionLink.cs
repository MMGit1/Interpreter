using System.Text.Json;
using Interpreter.Parser.BinaryOperations;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Expressions;

public class BinaryExpressionLink(
    IExpressionParser expressionParser,
    IBinaryOperationParser binaryOperationParser)
    : BaseExpressionParserLink
{
    public override ExpressionParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object ||
            !element.TryGetProperty("binop", out JsonElement operationElement))
        {
            return Next(element);
        }

        if (operationElement.ValueKind != JsonValueKind.String)
            return new ExpressionParseResult.Failure("Invalid binary operator");

        string? operation = operationElement.GetString();

        if (operation is null)
            return new ExpressionParseResult.Failure("Binary operator is null");

        if (!element.TryGetProperty("left", out JsonElement leftElement))
            return new ExpressionParseResult.Failure("Missing left expression");

        if (!element.TryGetProperty("right", out JsonElement rightElement))
            return new ExpressionParseResult.Failure("Missing right expression");

        var leftResult = expressionParser.Parse(leftElement);

        if (leftResult is not ExpressionParseResult.Success leftSuccess)
            return leftResult;

        var rightResult = expressionParser.Parse(rightElement);

        if (rightResult is not ExpressionParseResult.Success rightSuccess)
            return rightResult;

        return binaryOperationParser.Parse(
            operation,
            leftSuccess.Expression,
            rightSuccess.Expression
        );
    }
}