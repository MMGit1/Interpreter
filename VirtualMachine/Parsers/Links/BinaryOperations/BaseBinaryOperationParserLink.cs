using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links.BinaryOperations;

public abstract class BaseBinaryOperationParserLink : IBinaryOperationParserLink
{
    private IBinaryOperationParserLink? _next;

    public IBinaryOperationParserLink AddNext(IBinaryOperationParserLink next)
    {
        if (_next == null)
        {
            _next = next;
        }
        else
        {
            _next.AddNext(next);
        }

        return this;
    }

    public abstract CommandParsingResult Parse(IEnumerator<string> current);

    protected CommandParsingResult Next(IEnumerator<string> current)
    {
        return _next == null
            ? new CommandParsingResult.Failure("Unknown binary operation")
            : _next.Parse(current);
    }
}
