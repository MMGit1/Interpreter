using Interpreter.Ast.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.BinaryOperations;

public interface IBinaryOperationParser
{
    ExpressionParseResult Parse(
        string operation,
        IExpression left,
        IExpression right);
}