using VirtualMachine.Commands;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public sealed class LengthParserLink : BaseCommandParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        return current.Current == "LENGTH"
            ? new CommandParsingResult.Success(new Length())
            : Next(current);
    }
}
