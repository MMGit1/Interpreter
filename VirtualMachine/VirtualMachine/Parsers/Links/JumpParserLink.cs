using VirtualMachine.Commands;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public class JumpParserLink : BaseCommandParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        if (current.Current != "JMP")
        {
            return Next(current);
        }

        return current.MoveNext()
            ? new CommandParsingResult.Success(new Jump(current.Current))
            : new CommandParsingResult.Failure("JMP requires a label");
    }
}
