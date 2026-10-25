namespace VirtualMachine.Parsers.Links.BinaryOperations;

public class BinaryOperationChainFactory : IBinaryOperationChainFactory
{
    public IBinaryOperationParserLink Create()
    {
        return new AddParserLink()
            .AddNext(new ConsParserLink())
            .AddNext(new SubtractParserLink())
            .AddNext(new MultiplyParserLink())
            .AddNext(new DivideParserLink())
            .AddNext(new ModuloParserLink())
            .AddNext(new EqualParserLink())
            .AddNext(new NotEqualParserLink())
            .AddNext(new LessParserLink())
            .AddNext(new LessOrEqualParserLink())
            .AddNext(new GreaterParserLink())
            .AddNext(new GreaterOrEqualParserLink())
            .AddNext(new AndParserLink())
            .AddNext(new OrParserLink());
    }
}
