using VirtualMachine.Commands;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public sealed class ArrayParserLink : BaseCommandParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        if (current.Current != "ARRAY")
        {
            return Next(current);
        }

        if (!current.MoveNext() || !int.TryParse(current.Current, out int length))
        {
            return new CommandParsingResult.Failure("ARRAY requires an integer argument");
        }

        return new CommandParsingResult.Success(new ArrayInstruction(length));
    }
}
