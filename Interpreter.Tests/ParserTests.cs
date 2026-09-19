using System.Text.Json;
using Interpreter.Ast.Expressions.BinaryExpressions;
using Interpreter.Ast.Statements;
using Interpreter.Parser.BinaryOperations;
using Interpreter.Parser.Expressions;
using Interpreter.Parser.Results;
using Interpreter.Parser.Statements;

namespace Interpreter.Tests;

public class ParserTests
{
    [Fact]
    public void ParseAssignment()
    {
        const string json = """
                            {
                              "assn": {
                                "dst": "x",
                                "src": {
                                  "const": 5
                                }
                              }
                            }
                            """;

        var result = ParseStatement(json);

        var success = Assert.IsType<StatementParseResult.Success>(result);
        Assert.IsType<Assignment>(success.Statement);
    }

    [Fact]
    public void ParseSequence()
    {
        const string json = """
                            {
                              "seq": {
                                "left": {
                                  "assn": {
                                    "dst": "x",
                                    "src": {
                                      "const": 1
                                    }
                                  }
                                },
                                "right": {
                                  "write": {
                                    "var": "x"
                                  }
                                }
                              }
                            }
                            """;

        var result = ParseStatement(json);

        var success = Assert.IsType<StatementParseResult.Success>(result);
        Assert.IsType<Sequence>(success.Statement);
    }

    [Fact]
    public void ParseRead()
    {
        const string json = """
                            {
                              "read": "x"
                            }
                            """;

        var result = ParseStatement(json);

        var success = Assert.IsType<StatementParseResult.Success>(result);
        Assert.IsType<Read>(success.Statement);
    }

    [Fact]
    public void ParseWrite()
    {
        const string json = """
                            {
                              "write": {
                                "const": 10
                              }
                            }
                            """;

        var result = ParseStatement(json);

        var success = Assert.IsType<StatementParseResult.Success>(result);
        Assert.IsType<Write>(success.Statement);
    }

    [Fact]
    public void ParseSkip()
    {
        const string json = """
                            "skip"
                            """;

        var result = ParseStatement(json);

        var success = Assert.IsType<StatementParseResult.Success>(result);
        Assert.IsType<Skip>(success.Statement);
    }

    [Fact]
    public void ParseIf()
    {
        const string json = """
                            {
                              "if": {
                                "cond": {
                                  "const": 1
                                },
                                "then": {
                                  "write": {
                                    "const": 10
                                  }
                                },
                                "else": {
                                  "write": {
                                    "const": 20
                                  }
                                }
                              }
                            }
                            """;

        var result = ParseStatement(json);

        var success = Assert.IsType<StatementParseResult.Success>(result);
        Assert.IsType<If>(success.Statement);
    }

    [Fact]
    public void ParseWhile()
    {
        const string json = """
                            {
                              "while": {
                                "cond": {
                                  "const": 1
                                },
                                "body": "skip"
                              }
                            }
                            """;

        var result = ParseStatement(json);

        var success = Assert.IsType<StatementParseResult.Success>(result);
        Assert.IsType<While>(success.Statement);
    }

    [Fact]
    public void ParseDoWhile()
    {
        const string json = """
                            {
                              "do": {
                                "body": "skip",
                                "cond": {
                                  "const": 0
                                }
                              }
                            }
                            """;

        var result = ParseStatement(json);

        var success = Assert.IsType<StatementParseResult.Success>(result);
        Assert.IsType<DoWhile>(success.Statement);
    }

    [Fact]
    public void ParseNestedBinaryExpression()
    {
        const string json = """
                            {
                              "binop": "+",
                              "left": {
                                "const": 2
                              },
                              "right": {
                                "binop": "*",
                                "left": {
                                  "const": 3
                                },
                                "right": {
                                  "const": 4
                                }
                              }
                            }
                            """;

        var parser = CreateExpressionParser();

        using JsonDocument document = JsonDocument.Parse(json);

        var result = parser.Parse(document.RootElement);

        var success = Assert.IsType<ExpressionParseResult.Success>(result);
        Assert.IsType<Add>(success.Expression);
    }

    [Fact]
    public void UnknownStatementReturnsFailure()
    {
        const string json = """
                            {
                              "unknown": 123
                            }
                            """;

        var result = ParseStatement(json);

        Assert.IsType<StatementParseResult.Failure>(result);
    }

    [Fact]
    public void AssignmentWithoutSourceReturnsFailure()
    {
        const string json = """
                            {
                              "assn": {
                                "dst": "x"
                              }
                            }
                            """;

        var result = ParseStatement(json);

        Assert.IsType<StatementParseResult.Failure>(result);
    }

    private static StatementParseResult ParseStatement(string json)
    {
        var parser = CreateStatementParser();

        using JsonDocument document = JsonDocument.Parse(json);

        return parser.Parse(document.RootElement);
    }

    private static StatementParser CreateStatementParser()
    {
        var expressionParser = CreateExpressionParser();

        return new StatementParser(
            new StatementChainFactory(expressionParser)
        );
    }

    private static ExpressionParser CreateExpressionParser()
    {
        var binaryOperationParser = new BinaryOperationParser(
            new BinaryOperationChainFactory()
        );

        return new ExpressionParser(
            new ExpressionChainFactory(binaryOperationParser)
        );
    }
}