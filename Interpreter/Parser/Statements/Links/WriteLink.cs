using System.Text.Json;
using Interpreter.Ast.Statements;
using Interpreter.Parser.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Statements.Links;

public class WriteLink(IExpressionParser expressionParser)
    : BaseStatementParserLink
{
    public override StatementParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object ||
            !element.TryGetProperty("write", out JsonElement expressionElement))
        {
            return Next(element);
        }

        var expressionResult = expressionParser.Parse(expressionElement);

        if (expressionResult is not ExpressionParseResult.Success expressionSuccess)
        {
            return new StatementParseResult.Failure(
                ((ExpressionParseResult.Failure)expressionResult).Message
            );
        }

        return new StatementParseResult.Success(
            new Write(expressionSuccess.Expression)
        );
    }
}