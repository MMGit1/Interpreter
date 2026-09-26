using VirtualMachine.Commands;
using VirtualMachine.Commands.BinaryOperators;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links.BinaryOperations;

public class LessOrEqualParserLink : BaseBinaryOperationParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        return current.Current == "<="
            ? new CommandParsingResult.Success(new BinaryOperationCommand(new LessOrEqual()))
            : Next(current);
    }
}
