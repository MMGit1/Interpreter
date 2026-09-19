using System.Text.Json;
using Interpreter.Ast.Statements;
using Interpreter.Parser.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Statements.Links;

public class IfLink(
    IStatementParser statementParser,
    IExpressionParser expressionParser)
    : BaseStatementParserLink
{
    public override StatementParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object ||
            !element.TryGetProperty("if", out JsonElement ifElement))
        {
            return Next(element);
        }

        if (!ifElement.TryGetProperty("cond", out JsonElement conditionElement))
            return new StatementParseResult.Failure("Missing if condition");

        if (!ifElement.TryGetProperty("then", out JsonElement thenElement))
            return new StatementParseResult.Failure("Missing then branch");

        if (!ifElement.TryGetProperty("else", out JsonElement elseElement))
            return new StatementParseResult.Failure("Missing else branch");

        var conditionResult = expressionParser.Parse(conditionElement);

        if (conditionResult is not ExpressionParseResult.Success conditionSuccess)
        {
            return new StatementParseResult.Failure(
                ((ExpressionParseResult.Failure)conditionResult).Message
            );
        }

        var thenResult = statementParser.Parse(thenElement);

        if (thenResult is not StatementParseResult.Success thenSuccess)
            return thenResult;

        var elseResult = statementParser.Parse(elseElement);

        if (elseResult is not StatementParseResult.Success elseSuccess)
            return elseResult;

        return new StatementParseResult.Success(
            new If(
                conditionSuccess.Expression,
                thenSuccess.Statement,
                elseSuccess.Statement
            )
        );
    }
}