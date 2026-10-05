using Compiler.Parsing;

namespace Compiler.Composition;

public interface IParserFactory
{
    ISourceParser Create();
}
