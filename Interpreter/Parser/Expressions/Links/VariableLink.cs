using System.Text.Json;
using Interpreter.Ast.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Expressions;

public class VariableLink : BaseExpressionParserLink
{
    public override ExpressionParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object ||
            !element.TryGetProperty("var", out JsonElement value))
        {
            return Next(element);
        }

        if (value.ValueKind != JsonValueKind.String)
            return new ExpressionParseResult.Failure("Invalid variable name");

        var name = value.GetString();

        if (name is null)
            return new ExpressionParseResult.Failure("Variable name is null");

        return new ExpressionParseResult.Success(new Variable(name));
    }
}