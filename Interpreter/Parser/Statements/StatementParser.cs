using System.Text.Json;
using Interpreter.Parser.Results;
using Interpreter.Parser.Statements.Links;

namespace Interpreter.Parser.Statements;

public class StatementParser : IStatementParser
{
    private readonly IStatementParserLink _root;

    public StatementParser(IStatementChainFactory chainFactory)
    {
        _root = chainFactory.Create(this);
    }

    public StatementParseResult Parse(JsonElement element)
    {
        return _root.Parse(element);
    }
}