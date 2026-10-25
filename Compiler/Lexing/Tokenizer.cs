namespace Compiler.Lexing;

public sealed class Tokenizer : ITokenizer
{
    public TokenizationResult Tokenize(string source)
    {
        var tokens = new List<Token>();
        var index = 0;
        var line = 1;
        var column = 1;

        while (index < source.Length)
        {
            var current = source[index];

            if (char.IsWhiteSpace(current))
            {
                Advance(source, ref index, ref line, ref column);
                continue;
            }

            if (current == '-' && Peek(source, index) == '-')
            {
                while (index < source.Length && source[index] != '\n')
                {
                    Advance(source, ref index, ref line, ref column);
                }

                continue;
            }

            if (current == '(' && Peek(source, index) == '*')
            {
                Advance(source, ref index, ref line, ref column);
                Advance(source, ref index, ref line, ref column);

                while (index < source.Length && !(source[index] == '*' && Peek(source, index) == ')'))
                {
                    Advance(source, ref index, ref line, ref column);
                }

                if (index == source.Length)
                {
                    return new TokenizationResult.Failure($"Unclosed comment at ({line}:{column})");
                }

                Advance(source, ref index, ref line, ref column);
                Advance(source, ref index, ref line, ref column);
                continue;
            }

            var tokenLine = line;
            var tokenColumn = column;

            if (char.IsLower(current) || char.IsUpper(current))
            {
                var start = index;
                do
                {
                    Advance(source, ref index, ref line, ref column);
                }
                while (index < source.Length &&
                       (char.IsLetterOrDigit(source[index]) || source[index] is '_' or '\''));

                var text = source[start..index];
                tokens.Add(new Token(text, tokenLine, tokenColumn));
                continue;
            }

            if (current == '\'')
            {
                var start = index;
                Advance(source, ref index, ref line, ref column);

                while (index < source.Length && source[index] != '\'')
                {
                    if (source[index] == '\\')
                    {
                        Advance(source, ref index, ref line, ref column);
                    }

                    if (index == source.Length)
                    {
                        break;
                    }

                    Advance(source, ref index, ref line, ref column);
                }

                if (index == source.Length)
                {
                    return new TokenizationResult.Failure(
                        $"Unclosed character at ({tokenLine}:{tokenColumn})"
                    );
                }

                Advance(source, ref index, ref line, ref column);
                tokens.Add(new Token(source[start..index], tokenLine, tokenColumn));
                continue;
            }

            if (char.IsDigit(current))
            {
                var start = index;
                while (index < source.Length && char.IsDigit(source[index]))
                {
                    Advance(source, ref index, ref line, ref column);
                }

                tokens.Add(new Token(source[start..index], tokenLine, tokenColumn));
                continue;
            }

            if (current == '"')
            {
                var start = index;
                Advance(source, ref index, ref line, ref column);

                while (index < source.Length && source[index] != '"')
                {
                    if (source[index] == '\\')
                    {
                        Advance(source, ref index, ref line, ref column);
                    }

                    if (index == source.Length)
                    {
                        break;
                    }

                    Advance(source, ref index, ref line, ref column);
                }

                if (index == source.Length)
                {
                    return new TokenizationResult.Failure(
                        $"Unclosed string at ({tokenLine}:{tokenColumn})"
                    );
                }

                Advance(source, ref index, ref line, ref column);
                tokens.Add(new Token(source[start..index], tokenLine, tokenColumn));
                continue;
            }

            var pair = index + 1 < source.Length ? source[index..(index + 2)] : string.Empty;
            if (IsTwoCharacterToken(pair))
            {
                tokens.Add(new Token(pair, tokenLine, tokenColumn));
                Advance(source, ref index, ref line, ref column);
                Advance(source, ref index, ref line, ref column);
                continue;
            }

            if (IsSingleCharacterToken(current))
            {
                tokens.Add(new Token(current.ToString(), tokenLine, tokenColumn));
                Advance(source, ref index, ref line, ref column);
                continue;
            }

            return new TokenizationResult.Failure($"Unexpected character '{current}' at ({line}:{column})");
        }

        tokens.Add(new Token(string.Empty, line, column));
        return new TokenizationResult.Success(tokens);
    }

    private static bool IsTwoCharacterToken(string text) =>
        text is "!!" or "&&" or "==" or "!=" or "<=" or ">=" or "+=" or "-=" or "*=" or "/=" or "%=";

    private static bool IsSingleCharacterToken(char character) =>
        character is '{' or '}' or '[' or ']' or '(' or ')' or ';' or ',' or '.' or ':' or '=' or '<' or '>' or '+' or '-' or '*' or '/' or '%';

    private static char? Peek(string source, int index) => index + 1 < source.Length ? source[index + 1] : null;

    private static void Advance(string source, ref int index, ref int line, ref int column)
    {
        if (source[index++] == '\n')
        {
            line++;
            column = 1;
        }
        else
        {
            column++;
        }
    }
}
