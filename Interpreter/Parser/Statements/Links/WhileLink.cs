using System.Text.Json;
using Interpreter.Ast.Statements;
using Interpreter.Parser.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Statements.Links;

public class WhileLink(
    IStatementParser statementParser,
    IExpressionParser expressionParser)
    : BaseStatementParserLink
{
    public override StatementParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object ||
            !element.TryGetProperty("while", out JsonElement whileElement))
        {
            return Next(element);
        }

        if (!whileElement.TryGetProperty("cond", out JsonElement conditionElement))
            return new StatementParseResult.Failure("Missing while condition");

        if (!whileElement.TryGetProperty("body", out JsonElement bodyElement))
            return new StatementParseResult.Failure("Missing while body");

        var conditionResult = expressionParser.Parse(conditionElement);

        if (conditionResult is not ExpressionParseResult.Success conditionSuccess)
        {
            return new StatementParseResult.Failure(
                ((ExpressionParseResult.Failure)conditionResult).Message
            );
        }

        var bodyResult = statementParser.Parse(bodyElement);

        if (bodyResult is not StatementParseResult.Success bodySuccess)
            return bodyResult;

        return new StatementParseResult.Success(
            new While(
                conditionSuccess.Expression,
                bodySuccess.Statement
            )
        );
    }
}