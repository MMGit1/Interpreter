using Compiler.CodeGeneration;
using Compiler.Lexing;
using Compiler.Parsing;
using Compiler.Parsing.Results;

namespace Compiler;

public sealed class SourceCompiler(
    ITokenizer tokenizer,
    ISourceParser parser,
    ICodeGenerator codeGenerator)
{
    public CompilationResult Compile(string source)
    {
        var tokenization = tokenizer.Tokenize(source);
        if (tokenization is TokenizationResult.Failure tokenizationFailure)
        {
            return new CompilationResult.Failure(tokenizationFailure.Message);
        }

        var parsing = parser.Parse(((TokenizationResult.Success)tokenization).Tokens);
        if (parsing is ParsingResult.Failure parsingFailure)
        {
            return new CompilationResult.Failure(parsingFailure.ErrorMessage);
        }

        return codeGenerator.Generate(((ParsingResult.Success)parsing).Program);
    }
}
