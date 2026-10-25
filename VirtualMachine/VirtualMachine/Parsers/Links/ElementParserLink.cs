using VirtualMachine.Commands;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public sealed class ElementParserLink : BaseCommandParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        return current.Current == "ELEM"
            ? new CommandParsingResult.Success(new Element())
            : Next(current);
    }
}
