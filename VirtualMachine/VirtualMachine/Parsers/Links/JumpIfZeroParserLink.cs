using VirtualMachine.Commands;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public class JumpIfZeroParserLink : BaseCommandParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        if (current.Current != "JZ")
        {
            return Next(current);
        }

        return current.MoveNext()
            ? new CommandParsingResult.Success(new JumpIfZero(current.Current))
            : new CommandParsingResult.Failure("JZ requires a label");
    }
}
