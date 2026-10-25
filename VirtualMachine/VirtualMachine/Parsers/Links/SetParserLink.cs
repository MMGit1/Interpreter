using VirtualMachine.Commands;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public sealed class SetParserLink : BaseCommandParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        return current.Current == "SET"
            ? new CommandParsingResult.Success(new Set())
            : Next(current);
    }
}
