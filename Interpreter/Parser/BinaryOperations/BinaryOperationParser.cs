using Interpreter.Ast.Expressions;
using Interpreter.Parser.BinaryOperations.Links;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.BinaryOperations;

public class BinaryOperationParser : IBinaryOperationParser
{
    private readonly IBinaryOperationParserLink _root;

    public BinaryOperationParser(IBinaryOperationChainFactory chainFactory)
    {
        _root = chainFactory.Create();
    }

    public ExpressionParseResult Parse(
        string operation,
        IExpression left,
        IExpression right)
    {
        return _root.Parse(operation, left, right);
    }
}