using Interpreter.Parser.Expressions;
using Interpreter.Parser.Statements.Links;

namespace Interpreter.Parser.Statements;

public class StatementChainFactory(
    IExpressionParser expressionParser)
    : IStatementChainFactory
{
    public IStatementParserLink Create(IStatementParser parser)
    {
        return new AssignmentLink(expressionParser)
            .AddNext(new SequenceLink(parser))
            .AddNext(new ReadLink())
            .AddNext(new WriteLink(expressionParser))
            .AddNext(new SkipLink())
            .AddNext(new IfLink(parser, expressionParser))
            .AddNext(new WhileLink(parser, expressionParser))
            .AddNext(new DoWhileLink(parser, expressionParser));
    }
}