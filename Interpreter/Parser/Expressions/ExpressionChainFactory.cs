using Interpreter.Parser.BinaryOperations;

namespace Interpreter.Parser.Expressions;

public class ExpressionChainFactory(
    IBinaryOperationParser binaryOperationParser)
    : IExpressionChainFactory
{
    public IExpressionParserLink Create(IExpressionParser parser)
    {
        return new ConstantLink()
            .AddNext(new VariableLink())
            .AddNext(
            new BinaryExpressionLink(
                parser,
                binaryOperationParser
                )
            );
    }
}