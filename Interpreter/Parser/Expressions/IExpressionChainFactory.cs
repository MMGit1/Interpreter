namespace Interpreter.Parser.Expressions;

public interface IExpressionChainFactory
{
    IExpressionParserLink Create(IExpressionParser parser);
}