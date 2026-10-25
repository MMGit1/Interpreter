using VirtualMachine.Commands;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers.Links;

public sealed class SExpressionParserLink : BaseCommandParserLink
{
    public override CommandParsingResult Parse(IEnumerator<string> current)
    {
        if (current.Current != "SEXP")
        {
            return Next(current);
        }

        if (!current.MoveNext())
        {
            return new CommandParsingResult.Failure("SEXP requires a name and argument count");
        }

        string name = current.Current;

        if (!current.MoveNext() || !int.TryParse(current.Current, out int argumentCount))
        {
            return new CommandParsingResult.Failure("SEXP requires a name and argument count");
        }

        return new CommandParsingResult.Success(
            new SExpression(name, argumentCount)
        );
    }
}
