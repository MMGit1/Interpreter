using VirtualMachine.Parsers.Links;
using VirtualMachine.Parsers.Links.BinaryOperations;

namespace VirtualMachine.Parsers;

public class CommandChainFactory : ICommandChainFactory
{
    public ICommandParserLink Create()
    {
        return new ReadParserLink()
            .AddNext(new WriteParserLink())
            .AddNext(new LoadParserLink())
            .AddNext(new StoreParserLink())
            .AddNext(new ConstParserLink())
            .AddNext(new ArrayParserLink())
            .AddNext(new StringParserLink())
            .AddNext(new LengthParserLink())
            .AddNext(new ElementParserLink())
            .AddNext(new SetParserLink())
            .AddNext(new SExpressionParserLink())
            .AddNext(new LabelParserLink())
            .AddNext(new JumpParserLink())
            .AddNext(new JumpIfZeroParserLink())
            .AddNext(new JumpIfNotZeroParserLink())
            .AddNext(
                new BinaryOperationParserLink(
                    new BinaryOperationChainFactory()
                )
            );
    }
}
