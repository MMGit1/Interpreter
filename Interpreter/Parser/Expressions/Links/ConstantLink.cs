using System.Text.Json;
using Interpreter.Ast.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Expressions;

public class ConstantLink : BaseExpressionParserLink
{
     public override ExpressionParseResult Parse(JsonElement element)
     {
          if (element.ValueKind != JsonValueKind.Object ||
              !element.TryGetProperty("const", out JsonElement value))
          {
               return Next(element);
          }

          if (!value.TryGetInt32(out int number))
               return new ExpressionParseResult.Failure("Invalid constant value");

          return new ExpressionParseResult.Success(new Constant(number));
     }
}