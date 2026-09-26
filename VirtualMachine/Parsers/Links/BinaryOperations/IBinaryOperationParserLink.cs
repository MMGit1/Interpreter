using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links.BinaryOperations;

public interface IBinaryOperationParserLink
{
    IBinaryOperationParserLink AddNext(IBinaryOperationParserLink next);

    CommandParsingResult Parse(IEnumerator<string> current);
}
