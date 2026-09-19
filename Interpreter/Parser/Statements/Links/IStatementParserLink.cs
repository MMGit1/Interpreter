using System.Text.Json;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Statements.Links;

public interface IStatementParserLink
{
    IStatementParserLink AddNext(IStatementParserLink nextLink);

    StatementParseResult Parse(JsonElement element);
}