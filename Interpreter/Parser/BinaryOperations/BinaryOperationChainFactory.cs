using Interpreter.Parser.BinaryOperations.Links;

namespace Interpreter.Parser.BinaryOperations;

public class BinaryOperationChainFactory : IBinaryOperationChainFactory
{
    public IBinaryOperationParserLink Create()
    {
        return new AddLink()
            .AddNext(new SubtractLink())
            .AddNext(new MultiplyLink())
            .AddNext(new DivideLink())
            .AddNext(new ModuloLink())
            .AddNext(new EqualLink())
            .AddNext(new NotEqualLink())
            .AddNext(new LessLink())
            .AddNext(new LessOrEqualLink())
            .AddNext(new GreaterLink())
            .AddNext(new GreaterOrEqualLink())
            .AddNext(new AndLink())
            .AddNext(new OrLink());
    }
}