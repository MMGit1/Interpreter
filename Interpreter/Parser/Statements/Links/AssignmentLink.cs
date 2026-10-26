using System.Text.Json;
using Interpreter.Ast.Statements;
using Interpreter.Ast.Expressions;
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
        {
            return new StatementParseResult.Failure("Missing assignment destination");
        }

        if (!assignmentElement.TryGetProperty("src", out JsonElement sourceElement))
        {
            return new StatementParseResult.Failure("Missing assignment source");
        }

        var sourceResult = expressionParser.Parse(sourceElement);

        if (sourceResult is not ExpressionParseResult.Success sourceSuccess)
            return new StatementParseResult.Failure(
                ((ExpressionParseResult.Failure)sourceResult).Message
            );

        if (destinationElement.ValueKind == JsonValueKind.String)
        {
            return new StatementParseResult.Success(
                new Assignment(
                    destinationElement.GetString()!,
                    sourceSuccess.Expression
                )
            );
        }

        var destinationResult = expressionParser.Parse(destinationElement);

        if (destinationResult is not ExpressionParseResult.Success destinationSuccess)
        {
            return new StatementParseResult.Failure(
                ((ExpressionParseResult.Failure)destinationResult).Message
            );
        }

        if (destinationSuccess.Expression is not ArrayElementAccess access)
        {
            return new StatementParseResult.Failure(
                "Assignment destination must be a variable or array element"
            );
        }

        return new StatementParseResult.Success(
            new ArrayAssignment(access, sourceSuccess.Expression)
        );
    }
}
