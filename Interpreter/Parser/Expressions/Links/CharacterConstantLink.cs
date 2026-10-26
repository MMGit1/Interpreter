using System.Text.Json;
using Interpreter.Ast.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Expressions;

public sealed class CharacterConstantLink : BaseExpressionParserLink
{
    public override ExpressionParseResult Parse(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object ||
            !element.TryGetProperty("char", out JsonElement value))
        {
            return Next(element);
        }

        if (value.ValueKind != JsonValueKind.String)
        {
            return new ExpressionParseResult.Failure("Invalid character value");
        }

        string character = value.GetString()!;

        if (character.Length != 1)
        {
            return new ExpressionParseResult.Failure("Character value must contain one character");
        }

        return new ExpressionParseResult.Success(
            new CharacterConstant(character)
        );
    }
}
