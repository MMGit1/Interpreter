using Compiler.Parsing.Ast.Statements;

namespace Compiler.Parsing.StatementParsing;

public interface IStatementParserLink
{
    IStatementParserLink AddNext(IStatementParserLink next);

    bool TryParse(SourceParser parser, out IStatementNode? statement);
}
