using Interpreter.Parser.Statements.Links;

namespace Interpreter.Parser.Statements;

public interface IStatementChainFactory
{
    IStatementParserLink Create(IStatementParser parser);
}