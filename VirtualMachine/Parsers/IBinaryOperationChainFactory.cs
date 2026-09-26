using VirtualMachine.Parsers.Links.BinaryOperations;

namespace VirtualMachine.Parsers;

public interface IBinaryOperationChainFactory
{
    IBinaryOperationParserLink Create();
}
