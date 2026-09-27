using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links.BinaryOperations;

public class BinaryOperationParserLink(
    IBinaryOperationChainFactory chainFactory) : BaseCommandParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        if (current.Current == "BINOP")
        {
            IBinaryOperationParserLink chain = chainFactory.Create();

            if (!current.MoveNext())
            {
                return new CommandParsingResult.Failure("BINOP requires an operator");
            }

            return chain.Parse(current);
        }
        
        return Next(current);
    }
}
