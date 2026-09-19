using Interpreter.Ast.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.BinaryOperations.Links;

public interface IBinaryOperationParserLink
{
    IBinaryOperationParserLink AddNext(IBinaryOperationParserLink nextLink);

    ExpressionParseResult Parse(
        string operation,
        IExpression left,
        IExpression right);
}