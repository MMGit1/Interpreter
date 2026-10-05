namespace Compiler.Parsing.StatementParsing;

public interface IStatementParserChainFactory
{
    IStatementParserLink Create();
}
