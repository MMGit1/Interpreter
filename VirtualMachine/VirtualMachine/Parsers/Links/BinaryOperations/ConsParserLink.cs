using VirtualMachine.Commands;
using VirtualMachine.Commands.BinaryOperators;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links.BinaryOperations;

public sealed class ConsParserLink : BaseBinaryOperationParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        return current.Current == ":"
            ? new CommandParsingResult.Success(new BinaryOperationCommand(new Cons()))
            : Next(current);
    }
}
