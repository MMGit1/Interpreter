using System.Text.Json;
using Interpreter.Parser.Results;

namespace Interpreter.Parser.Statements;

public interface IStatementParser
{
    StatementParseResult Parse(JsonElement element);
}