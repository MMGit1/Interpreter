using VirtualMachine.Commands;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public class LoadParserLink : BaseCommandParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        if (current.Current != "LD")
        {
            return Next(current);
        }

        return current.MoveNext()
            ? new CommandParsingResult.Success(new Load(current.Current))
            : new CommandParsingResult.Failure("LD requires a variable name");
    }
}
