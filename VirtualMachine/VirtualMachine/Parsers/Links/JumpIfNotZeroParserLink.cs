using VirtualMachine.Commands;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public class JumpIfNotZeroParserLink : BaseCommandParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        if (current.Current != "JNZ")
        {
            return Next(current);
        }

        return current.MoveNext()
            ? new CommandParsingResult.Success(new JumpIfNotZero(current.Current))
            : new CommandParsingResult.Failure("JNZ requires a label");
    }
}
