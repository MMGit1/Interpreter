using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public interface ICommandParserLink
{
    ICommandParserLink AddNext(ICommandParserLink next);

    CommandParsingResult Parse(IEnumerator<string> current);
}