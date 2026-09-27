using VirtualMachine.Commands;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public class ConstParserLink : BaseCommandParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        if (current.Current != "CONST")
        {
            return Next(current);
        }

        if (!current.MoveNext() || !int.TryParse(current.Current, out var value))
        {
            return new CommandParsingResult.Failure("CONST requires an integer argument");
        }

        return new CommandParsingResult.Success(new Const(value));
    }
}
