using System.Text.Json;
using Interpreter.Ast.Statements;
using Interpreter.Parser.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Statements.Links;

public class AssignmentLink(IExpressionParser expressionParser)
    : BaseStatementParserLink
{
    public override StatementParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object ||
            !element.TryGetProperty("assn", out JsonElement assignmentElement))
        {
            return Next(element);
        }

        if (!assignmentElement.TryGetProperty("dst", out JsonElement destinationElement))
            return new StatementParseResult.Failure("Missing assignment destination");

        if (destinationElement.ValueKind != JsonValueKind.String)
            return new StatementParseResult.Failure("Invalid assignment destination");

        string? destination = destinationElement.GetString();

        if (destination is null)
            return new StatementParseResult.Failure("Assignment destination is null");

        if (!assignmentElement.TryGetProperty("src", out JsonElement sourceElement))
            return new StatementParseResult.Failure("Missing assignment source");

        var sourceResult = expressionParser.Parse(sourceElement);

        if (sourceResult is not ExpressionParseResult.Success sourceSuccess)
            return new StatementParseResult.Failure(
                ((ExpressionParseResult.Failure)sourceResult).Message
            );

        return new StatementParseResult.Success(
            new Assignment(
                destination,
                sourceSuccess.Expression
            )
        );
    }
}