using System.Text.Json;
using Interpreter.Ast.Statements;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Statements.Links;

public class SkipLink : BaseStatementParserLink
{
    public override StatementParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.String)
            return Next(element);

        string? value = element.GetString();

        if (value != "skip")
            return Next(element);

        return new StatementParseResult.Success(
            new Skip()
        );
    }
}