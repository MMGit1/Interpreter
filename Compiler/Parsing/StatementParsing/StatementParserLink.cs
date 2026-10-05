using Compiler.Parsing.Ast.Statements;

namespace Compiler.Parsing.StatementParsing;

public sealed class StatementParserLink(
    Func<SourceParser, bool> canParse,
    Func<SourceParser, IStatementNode> parse) : IStatementParserLink
{
    private IStatementParserLink? _next;

    public IStatementParserLink AddNext(IStatementParserLink next)
    {
        if (_next is null)
        {
            _next = next;
        }
        else
        {
            _next.AddNext(next);
        }

        return this;
    }

    public bool TryParse(SourceParser parser, out IStatementNode? statement)
    {
        if (canParse(parser))
        {
            statement = parse(parser);
            return true;
        }

        if (_next is not null)
        {
            return _next.TryParse(parser, out statement);
        }

        statement = null;
        return false;
    }
}
