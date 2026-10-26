using System.Text.Json;
using Interpreter.Ast.Expressions;
using Interpreter.Ast.Statements;
using Interpreter.Parser.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Statements.Links;

public sealed class ArrayAssignmentLink(IExpressionParser expressionParser)
    : BaseStatementParserLink
{
    public override StatementParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object ||
            !element.TryGetProperty("set", out JsonElement setElement))
        {
            return Next(element);
        }

        if (!setElement.TryGetProperty("context", out JsonElement contextElement) ||
            !setElement.TryGetProperty("index", out JsonElement indexElement) ||
            !setElement.TryGetProperty("value", out JsonElement valueElement))
        {
            return new StatementParseResult.Failure(
                "Array assignment requires context, index and value"
            );
        }

        var contextResult = expressionParser.Parse(contextElement);

        if (contextResult is not ExpressionParseResult.Success contextSuccess)
        {
            return new StatementParseResult.Failure(
                ((ExpressionParseResult.Failure)contextResult).Message
            );
        }

        var indexResult = expressionParser.Parse(indexElement);

        if (indexResult is not ExpressionParseResult.Success indexSuccess)
        {
            return new StatementParseResult.Failure(
                ((ExpressionParseResult.Failure)indexResult).Message
            );
        }

        var valueResult = expressionParser.Parse(valueElement);

        if (valueResult is not ExpressionParseResult.Success valueSuccess)
        {
            return new StatementParseResult.Failure(
                ((ExpressionParseResult.Failure)valueResult).Message
            );
        }

        var destination = new ArrayElementAccess(
            contextSuccess.Expression,
            indexSuccess.Expression
        );

        return new StatementParseResult.Success(
            new ArrayAssignment(destination, valueSuccess.Expression)
        );
    }
}
