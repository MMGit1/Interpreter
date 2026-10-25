using VirtualMachine.Commands;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public sealed class StringParserLink : BaseCommandParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        if (current.Current != "STRING")
        {
            return Next(current);
        }

        if (!current.MoveNext())
        {
            return new CommandParsingResult.Failure("STRING requires a text argument");
        }

        return new CommandParsingResult.Success(
            new StringInstruction(current.Current)
        );
    }
}
