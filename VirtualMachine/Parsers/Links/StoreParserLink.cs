using VirtualMachine.Commands;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public class StoreParserLink : BaseCommandParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        if (current.Current == "ST")
        {
            return current.MoveNext()
                ? new CommandParsingResult.Success(new Store(current.Current))
                : new CommandParsingResult.Failure("ST requires a variable name");
        }
        
        return Next(current);
    }
}
