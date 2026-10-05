using Compiler.Parsing.Ast.Statements;

namespace Compiler.Parsing.StatementParsing;

public sealed class StatementParserChainFactory : IStatementParserChainFactory
{
    public IStatementParserLink Create()
    {
        return new StatementParserLink(
                parser => parser.IsCurrent("read"),
                parser => parser.ParseRead())
            .AddNext(new StatementParserLink(
                parser => parser.IsCurrent("write"),
                parser => parser.ParseWrite()))
            .AddNext(new StatementParserLink(
                parser => parser.IsCurrent("while"),
                parser => parser.ParseWhile()))
            .AddNext(new StatementParserLink(
                parser => parser.IsCurrent("do"),
                parser => parser.ParseDoWhile()))
            .AddNext(new StatementParserLink(
                parser => parser.IsCurrent("for"),
                parser => parser.ParseFor()))
            .AddNext(new StatementParserLink(
                parser => parser.IsCurrent("if"),
                parser => parser.ParseIf()))
            .AddNext(new StatementParserLink(
                parser => parser.IsCurrent("skip"),
                parser => parser.ParseSkip()))
            .AddNext(new StatementParserLink(
                parser => parser.IsCurrent("{"),
                parser => parser.ParseBlock()))
            .AddNext(new StatementParserLink(
                parser => parser.IsCurrentIdentifier(),
                parser => parser.ParseAssignment(true)));
    }
}
