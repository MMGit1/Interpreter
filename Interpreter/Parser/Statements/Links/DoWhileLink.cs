using System.Text.Json;
using Interpreter.Ast.Statements;
using Interpreter.Parser.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Statements.Links;

public class DoWhileLink(
    IStatementParser statementParser,
    IExpressionParser expressionParser)
    : BaseStatementParserLink
{
    public override StatementParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object ||
            !element.TryGetProperty("do", out JsonElement doElement))
        {
            return Next(element);
        }

        if (!doElement.TryGetProperty("body", out JsonElement bodyElement))
            return new StatementParseResult.Failure("Missing do-while body");

        if (!doElement.TryGetProperty("cond", out JsonElement conditionElement))
            return new StatementParseResult.Failure("Missing do-while condition");

        var bodyResult = statementParser.Parse(bodyElement);

        if (bodyResult is not StatementParseResult.Success bodySuccess)
            return bodyResult;

        var conditionResult = expressionParser.Parse(conditionElement);

        if (conditionResult is not ExpressionParseResult.Success conditionSuccess)
        {
            return new StatementParseResult.Failure(
                ((ExpressionParseResult.Failure)conditionResult).Message
            );
        }

        return new StatementParseResult.Success(
            new DoWhile(
                bodySuccess.Statement,
                conditionSuccess.Expression
            )
        );
    }
}