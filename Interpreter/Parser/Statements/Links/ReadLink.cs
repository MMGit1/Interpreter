using System.Text.Json;
using Interpreter.Ast.Statements;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Statements.Links;

public class ReadLink : BaseStatementParserLink
{
    public override StatementParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object ||
            !element.TryGetProperty("read", out JsonElement variableElement))
        {
            return Next(element);
        }

        if (variableElement.ValueKind != JsonValueKind.String)
            return new StatementParseResult.Failure("Invalid read variable");

        string? variableName = variableElement.GetString();

        if (variableName is null)
            return new StatementParseResult.Failure("Read variable is null");

        return new StatementParseResult.Success(
            new Read(variableName)
        );
    }
}