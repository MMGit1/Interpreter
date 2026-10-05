namespace Compiler.Lexing;

public interface ITokenizer
{
    TokenizationResult Tokenize(string source);
}
