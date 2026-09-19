using Interpreter.Ast.Expressions;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.BinaryOperations.Links;

public abstract class BaseBinaryOperationParserLink : IBinaryOperationParserLink
{
    private IBinaryOperationParserLink? _nextLink;

    public IBinaryOperationParserLink AddNext(IBinaryOperationParserLink nextLink)
    {
        if (_nextLink is null)
        {
            _nextLink = nextLink;
        }
        else
        {
            _nextLink.AddNext(nextLink);
        }

        return this;
    }

    public abstract ExpressionParseResult Parse(
        string operation,
        IExpression left,
        IExpression right);

    protected ExpressionParseResult Next(
        string operation,
        IExpression left,
        IExpression right)
    {
        return _nextLink is null
            ? new ExpressionParseResult.Failure(
                $"Unknown binary operator: {operation}")
            : _nextLink.Parse(operation, left, right);
    }
}