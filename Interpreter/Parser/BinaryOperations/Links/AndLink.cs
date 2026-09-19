using Interpreter.Ast.Expressions;
using Interpreter.Ast.Expressions.BinaryExpressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.BinaryOperations.Links;

public class AndLink : BaseBinaryOperationParserLink
{
    public override ExpressionParseResult Parse(
        string operation,
        IExpression left,
        IExpression right)
    {
        if (operation != "&&")
            return Next(operation, left, right);

        return new ExpressionParseResult.Success(
            new And(left, right)
        );
    }
}