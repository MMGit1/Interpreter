using VirtualMachine.Commands;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public class WriteParserLink : BaseCommandParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        return current.Current == "WRITE"
            ? new CommandParsingResult.Success(new Write())
            : Next(current);
    }
}
