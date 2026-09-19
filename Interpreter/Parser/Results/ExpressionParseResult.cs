using Interpreter.Ast.Expressions;

namespace Interpreter.Parser.Results;

public abstract record ExpressionParseResult
{
    private ExpressionParseResult() { }

    public sealed record Success(IExpression Expression) : ExpressionParseResult;                                      

    public sealed record Failure(string Message) : ExpressionParseResult;
}
