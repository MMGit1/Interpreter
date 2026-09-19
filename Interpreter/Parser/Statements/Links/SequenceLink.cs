using System.Text.Json;
using Interpreter.Ast.Statements;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Statements.Links;

public class SequenceLink(IStatementParser statementParser)
    : BaseStatementParserLink
{
    public override StatementParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object ||
            !element.TryGetProperty("seq", out JsonElement sequenceElement))
        {
            return Next(element);
        }

        if (!sequenceElement.TryGetProperty("left", out JsonElement leftElement))
            return new StatementParseResult.Failure("Missing left statement");

        if (!sequenceElement.TryGetProperty("right", out JsonElement rightElement))
            return new StatementParseResult.Failure("Missing right statement");

        var leftResult = statementParser.Parse(leftElement);

        if (leftResult is not StatementParseResult.Success leftSuccess)
            return leftResult;

        var rightResult = statementParser.Parse(rightElement);

        if (rightResult is not StatementParseResult.Success rightSuccess)
            return rightResult;

        return new StatementParseResult.Success(
            new Sequence(
                leftSuccess.Statement,
                rightSuccess.Statement
            )
        );
    }
}