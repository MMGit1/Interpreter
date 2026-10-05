using Compiler.Lexing;
using Compiler.Parsing.Results;

namespace Compiler.Parsing;

public interface ISourceParser
{
    ParsingResult Parse(IReadOnlyList<Token> tokens);
}
