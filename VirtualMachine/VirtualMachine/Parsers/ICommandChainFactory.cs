using VirtualMachine.Parsers.Links;

namespace VirtualMachine.Parsers;

public interface ICommandChainFactory
{
    ICommandParserLink Create();
}