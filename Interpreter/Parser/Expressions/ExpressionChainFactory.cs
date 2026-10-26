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
            .AddNext(new StringConstantLink())
            .AddNext(new CharacterConstantLink())
            .AddNext(new ArrayLiteralLink(parser))
            .AddNext(new ArrayElementAccessLink(parser))
            .AddNext(new ArrayLengthLink(parser))
            .AddNext(new SExpressionLink(parser))
            .AddNext(
                new BinaryExpressionLink(
                    parser,
                    binaryOperationParser
                )
            );
    }
}
