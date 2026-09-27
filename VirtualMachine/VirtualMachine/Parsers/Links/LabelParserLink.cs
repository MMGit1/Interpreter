using VirtualMachine.Commands;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public class LabelParserLink : BaseCommandParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        if (current.Current != "LABEL")
        {
            return Next(current);
        }

        return current.MoveNext()
            ? new CommandParsingResult.Success(new Label(current.Current))
            : new CommandParsingResult.Failure("LABEL requires a name");
    }
}
