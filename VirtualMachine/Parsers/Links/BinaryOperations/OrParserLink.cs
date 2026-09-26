using VirtualMachine.Commands;
using VirtualMachine.Commands.BinaryOperators;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links.BinaryOperations;

public class OrParserLink : BaseBinaryOperationParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        if (current.Current == "!!")
        {
            return new CommandParsingResult.Success(new BinaryOperationCommand(new Or()));
        }
        
        return Next(current);
    }
}
