using Compiler.Lexing;
using Compiler.Parsing.Ast;
using Compiler.Parsing.Ast.Expressions;
using Compiler.Parsing.Ast.Statements;
using Compiler.Parsing.Operators;
using Compiler.Parsing.Results;
using Compiler.Parsing.StatementParsing;

namespace Compiler.Parsing;

public sealed class SourceParser(
    IOperatorCatalog operatorCatalog,
    IStatementParserChainFactory statementParserChainFactory) : ISourceParser
{
    private readonly IOperatorCatalog _operatorCatalog = operatorCatalog;
    private readonly IStatementParserLink _statementParserChain = statementParserChainFactory.Create();
    private IReadOnlyList<Token> _tokens = [];
    private int _position;

    public ParsingResult Parse(IReadOnlyList<Token> tokens)
    {
        _tokens = tokens;
        _position = 0;

        try
        {
            Consume("{", "'{' expected");
            var statements = ParseStatements();
            Consume("}", "'}' expected");
            Consume(string.Empty, "Unexpected text after program");

            return new ParsingResult.Success(new ProgramNode(statements));
        }
        catch (ParseException exception)
        {
            return new ParsingResult.Failure(exception.Message);
        }
    }

    private IReadOnlyList<IStatementNode> ParseStatements()
    {
        var statements = new List<IStatementNode>();
        do
        {
            statements.Add(ParseStatement());
        }

        while (!Check("}"));

        return statements;
    }

    private IStatementNode ParseStatement()
    {
        if (_statementParserChain.TryParse(this, out var statement))
        {
            return statement!;
        }

        return Fail<IStatementNode>("Statement expected");
    }

    public ReadNode ParseRead()
    {
        Advance();
        Consume("(", "'(' expected");
        var name = ConsumeIdentifier();
        Consume(")", "')' expected");
        OptionalSemicolon();
        return new ReadNode(name);
    }

    public WriteNode ParseWrite()
    {
        Advance();
        Consume("(", "'(' expected");
        var expression = ParseExpression();
        Consume(")", "')' expected");
        OptionalSemicolon();
        return new WriteNode(expression);
    }

    public AssignmentNode ParseAssignment(bool optionalSemicolon)
    {
        var name = ConsumeIdentifier();
        if (!_operatorCatalog.TryGetAssignment(Current.Text, out var operation))
        {
            throw Error("Assignment operator expected");
        }

        Advance();
        var expression = ParseExpression();

        if (optionalSemicolon)
        {
            OptionalSemicolon();
        }

        return new AssignmentNode(name, operation, expression);
    }

    public WhileNode ParseWhile()
    {
        Advance();
        return new WhileNode(ParseCondition(), ParseStatement());
    }

    public DoWhileNode ParseDoWhile()
    {
        Advance();
        var body = ParseStatement();
        Consume("while", "'while' expected");
        var condition = ParseCondition();
        OptionalSemicolon();
        return new DoWhileNode(body, condition);
    }

    public ForNode ParseFor()
    {
        Advance();
        Consume("(", "'(' expected");
        var initialization = ParseAssignment(false);
        Consume(";", "';' expected");
        var condition = ParseExpression();
        Consume(";", "';' expected");
        var update = ParseAssignment(false);
        Consume(")", "')' expected");

        return new ForNode(initialization, condition, update, ParseStatement());
    }

    public IfNode ParseIf()
    {
        Advance();
        return ParseIfTail();
    }

    private IfNode ParseIfTail()
    {
        var condition = ParseCondition();
        var then = ParseStatement();
        IStatementNode? otherwise = null;

        if (Match("else"))
        {
            otherwise = ParseStatement();
        }
        else if (Match("elif"))
        {
            otherwise = ParseIfTail();
        }

        return new IfNode(condition, then, otherwise);
    }

    public SkipNode ParseSkip()
    {
        Advance();
        OptionalSemicolon();
        return new SkipNode();
    }

    public BlockNode ParseBlock()
    {
        Advance();
        var statements = ParseStatements();
        Consume("}", "'}' expected");
        return new BlockNode(statements);
    }

    private IExpressionNode ParseCondition()
    {
        Consume("(", "'(' expected");
        var expression = ParseExpression();
        Consume(")", "')' expected");
        return expression;
    }

    private IExpressionNode ParseExpression()
    {
        return ParseOr();
    }

    private IExpressionNode ParseOr()
    {
        return ParseBinary(ParseAnd, ["!!"]);
    }

    private IExpressionNode ParseAnd()
    {
        return ParseBinary(ParseEquality, ["&&"]);
    }

    private IExpressionNode ParseEquality()
    {
        return ParseBinary(ParseRelation, ["==", "!="]);
    }

    private IExpressionNode ParseRelation()
    {
        return ParseBinary(ParseAdditive, ["<", "<=", ">", ">="]);
    }

    private IExpressionNode ParseAdditive()
    {
        return ParseBinary(ParseMultiplicative, ["+", "-"]);
    }

    private IExpressionNode ParseMultiplicative()
    {
        return ParseBinary(ParsePrimary, ["*", "/", "%"]);
    }

    private IExpressionNode ParseBinary(Func<IExpressionNode> next, IReadOnlyCollection<string> symbols)
    {
        var expression = next();
        while (symbols.Contains(Current.Text))
        {
            if (!_operatorCatalog.TryGetBinary(Current.Text, out var operation))
            {
                throw Error("Binary operator expected");
            }

            Advance();
            expression = new BinaryNode(expression, operation, next());
        }

        return expression;
    }

    private IExpressionNode ParsePrimary()
    {
        if (int.TryParse(Current.Text, out var value))
        {
            Advance();
            return new ConstantNode(value);
        }

        if (IsIdentifier(Current.Text))
        {
            return new VariableNode(Advance().Text);
        }

        if (Match("("))
        {
            var expression = ParseExpression();
            Consume(")", "')' expected");
            return expression;
        }

        return Fail<IExpressionNode>("Expression expected");
    }

    private string ConsumeIdentifier()
    {
        if (IsIdentifier(Current.Text))
        {
            return Advance().Text;
        }

        throw Error("Variable name expected");
    }

    private static bool IsIdentifier(string text)
    {
        return text.Length > 0 &&
               char.IsLower(text[0]) &&
               text.All(character => char.IsLetterOrDigit(character) || character is '_' or '\'');
    }

    private bool Match(string text)
    {
        if (!Check(text))
        {
            return false;
        }

        Advance();
        return true;
    }
    private bool Check(string text)
    {
        return Current.Text == text;
    }

    public bool IsCurrent(string text)
    {
        return Check(text);
    }

    public bool IsCurrentIdentifier()
    {
        return IsIdentifier(Current.Text);
    }
    private Token Consume(string text, string message)
    {
        if (Check(text))
        {
            return Advance();
        }

        throw Error(message);
    }
    private void OptionalSemicolon()
    {
        Match(";");
    }
    private Token Advance()
    {
        if (!Check(string.Empty))
        {
            _position++;
        }

        return Previous;
    }
    private Token Current => _tokens[_position];

    private Token Previous => _tokens[_position - 1];

    private ParseException Error(string message)
    {
        return new ParseException($"{message} at ({Current.Line}:{Current.Column})");
    }

    private T Fail<T>(string message)
    {
        throw Error(message);
    }
    private sealed class ParseException(string message) : Exception(message);
}
