using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public abstract class BaseCommandParserLink : ICommandParserLink
{
    private ICommandParserLink? _next;

    public ICommandParserLink AddNext(ICommandParserLink next)
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
            ? new CommandParsingResult.Failure("Unknown expression")
            : _next.Parse(current);
    }
}
