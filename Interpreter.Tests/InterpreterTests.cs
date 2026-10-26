using System.Text.Json;
using Interpreter.Context;
using Interpreter.Exceptions;
using Interpreter.Parser.BinaryOperations;
using Interpreter.Parser.Expressions;
using Interpreter.Parser.Results;
using Interpreter.Parser.Statements;

namespace Interpreter.Tests;

public class InterpreterTests
{
    [Fact]
    public void AssignmentAndWriteOutputsValue()
    {
        const string json = """
                            {
                              "seq": {
                                "left": {
                                  "assn": {
                                    "dst": "x",
                                    "src": {
                                      "const": 5
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

        var output = Execute(json);

        Assert.Equal($"5{Environment.NewLine}", output);
    }

    [Fact]
    public void ArithmeticExpressionOutputs14()
    {
        const string json = """
                            {
                              "write": {
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
                            }
                            """;

        var output = Execute(json);

        Assert.Equal($"14{Environment.NewLine}", output);
    }

    [Fact]
    public void ReadAndWriteOutputsInput()
    {
        const string json = """
                            {
                              "seq": {
                                "left": {
                                  "read": "x"
                                },
                                "right": {
                                  "write": {
                                    "var": "x"
                                  }
                                }
                              }
                            }
                            """;

        var output = Execute(json, "42");

        Assert.Equal($"42{Environment.NewLine}", output);
    }

    [Fact]
    public void IfTrueExecutesThenBranch()
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

        var output = Execute(json);

        Assert.Equal($"10{Environment.NewLine}", output);
    }

    [Fact]
    public void IfFalseExecutesElseBranch()
    {
        const string json = """
                            {
                              "if": {
                                "cond": {
                                  "const": 0
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

        var output = Execute(json);

        Assert.Equal($"20{Environment.NewLine}", output);
    }

    [Fact]
    public void WhileExecutesUntilConditionBecomesFalse()
    {
        const string json = """
                            {
                              "seq": {
                                "left": {
                                  "assn": {
                                    "dst": "x",
                                    "src": {
                                      "const": 0
                                    }
                                  }
                                },
                                "right": {
                                  "while": {
                                    "cond": {
                                      "binop": "<",
                                      "left": {
                                        "var": "x"
                                      },
                                      "right": {
                                        "const": 3
                                      }
                                    },
                                    "body": {
                                      "seq": {
                                        "left": {
                                          "write": {
                                            "var": "x"
                                          }
                                        },
                                        "right": {
                                          "assn": {
                                            "dst": "x",
                                            "src": {
                                              "binop": "+",
                                              "left": {
                                                "var": "x"
                                              },
                                              "right": {
                                                "const": 1
                                              }
                                            }
                                          }
                                        }
                                      }
                                    }
                                  }
                                }
                              }
                            }
                            """;

        var output = Execute(json);

        var expected =
            $"0{Environment.NewLine}" +
            $"1{Environment.NewLine}" +
            $"2{Environment.NewLine}";

        Assert.Equal(expected, output);
    }

    [Fact]
    public void DoWhileExecutesBodyAtLeastOnce()
    {
        const string json = """
                            {
                              "do": {
                                "body": {
                                  "write": {
                                    "const": 7
                                  }
                                },
                                "cond": {
                                  "const": 0
                                }
                              }
                            }
                            """;

        var output = Execute(json);

        Assert.Equal($"7{Environment.NewLine}", output);
    }

    [Fact]
    public void SkipProducesNoOutput()
    {
        const string json = """
                            "skip"
                            """;

        var output = Execute(json);

        Assert.Equal(string.Empty, output);
    }

    [Theory]
    [InlineData("<", 2, 3, 1)]
    [InlineData("<", 3, 2, 0)]
    [InlineData("<=", 2, 2, 1)]
    [InlineData(">", 3, 2, 1)]
    [InlineData(">=", 2, 3, 0)]
    [InlineData("==", 5, 5, 1)]
    [InlineData("==", 5, 6, 0)]
    [InlineData("!=", 5, 6, 1)]
    public void ComparisonOperatorsReturnZeroOrOne(
        string operation,
        int left,
        int right,
        int expected)
    {
        var json = $$"""
                     {
                       "write": {
                         "binop": "{{operation}}",
                         "left": {
                           "const": {{left}}
                         },
                         "right": {
                           "const": {{right}}
                         }
                       }
                     }
                     """;

        var output = Execute(json);

        Assert.Equal(
            $"{expected}{Environment.NewLine}",
            output
        );
    }

    [Theory]
    [InlineData("&&", 1, 2, 1)]
    [InlineData("&&", 1, 0, 0)]
    [InlineData("!!", 0, 5, 1)]
    [InlineData("!!", 0, 0, 0)]
    public void LogicalOperatorsUseIntegerTruthiness(
        string operation,
        int left,
        int right,
        int expected)
    {
        var json = $$"""
                     {
                       "write": {
                         "binop": "{{operation}}",
                         "left": {
                           "const": {{left}}
                         },
                         "right": {
                           "const": {{right}}
                         }
                       }
                     }
                     """;

        var output = Execute(json);

        Assert.Equal(
            $"{expected}{Environment.NewLine}",
            output
        );
    }

    [Fact]
    public void UndefinedVariableThrowsException()
    {
        const string json = """
                            {
                              "write": {
                                "var": "x"
                              }
                            }
                            """;

        Assert.Throws<UndefinedVariableException>(
            () => Execute(json)
        );
    }

    [Fact]
    public void DivisionByZeroThrowsException()
    {
        const string json = """
                            {
                              "write": {
                                "binop": "/",
                                "left": {
                                  "const": 10
                                },
                                "right": {
                                  "const": 0
                                }
                              }
                            }
                            """;

        Assert.Throws<DivisionByZeroException>(
            () => Execute(json)
        );
    }

    [Fact]
    public void ModuloByZeroThrowsException()
    {
        const string json = """
                            {
                              "write": {
                                "binop": "%",
                                "left": {
                                  "const": 10
                                },
                                "right": {
                                  "const": 0
                                }
                              }
                            }
                            """;

        Assert.Throws<DivisionByZeroException>(
            () => Execute(json)
        );
    }

    [Fact]
    public void ReadWithoutInputThrowsException()
    {
        const string json = """
                            {
                              "read": "x"
                            }
                            """;

        Assert.Throws<NoInputException>(
            () => Execute(json)
        );
    }

    [Fact]
    public void AndDoesNotShortCircuit()
    {
        const string json = """
                            {
                              "write": {
                                "binop": "&&",
                                "left": {
                                  "const": 0
                                },
                                "right": {
                                  "binop": "/",
                                  "left": {
                                    "const": 1
                                  },
                                  "right": {
                                    "const": 0
                                  }
                                }
                              }
                            }
                            """;

        Assert.Throws<DivisionByZeroException>(
            () => Execute(json)
        );
    }

    [Fact]
    public void OrDoesNotShortCircuit()
    {
        const string json = """
                            {
                              "write": {
                                "binop": "!!",
                                "left": {
                                  "const": 1
                                },
                                "right": {
                                  "binop": "/",
                                  "left": {
                                    "const": 1
                                  },
                                  "right": {
                                    "const": 0
                                  }
                                }
                              }
                            }
                            """;

        Assert.Throws<DivisionByZeroException>(
            () => Execute(json)
        );
    }

    [Fact]
    public void StringValueCanBeWritten()
    {
        // Arrange
        const string json = """
                            {
                              "write": { "string": "hello" }
                            }
                            """;

        // Act
        var output = Execute(json);

        // Assert
        Assert.Equal($"hello{Environment.NewLine}", output);
    }

    [Fact]
    public void EqualReturnsOneForEqualStrings()
    {
        // Arrange
        const string json = """
                            {
                              "write": {
                                "binop": "==",
                                "left": { "string": "hello" },
                                "right": { "string": "hello" }
                              }
                            }
                            """;

        // Act
        var output = Execute(json);

        // Assert
        Assert.Equal($"1{Environment.NewLine}", output);
    }

    [Fact]
    public void CharacterSExpressionAndConsCanBeWritten()
    {
        // Arrange
        const string json = """
                            {
                              "seq": {
                                "left": {
                                  "write": {
                                    "binop": ":",
                                    "left": { "char": "x" },
                                    "right": {
                                      "array": [{ "const": 1 }, { "const": 2 }]
                                    }
                                  }
                                },
                                "right": {
                                  "write": {
                                    "sexp": {
                                      "name": "Node",
                                      "arguments": [
                                        { "const": 1 },
                                        { "string": "value" }
                                      ]
                                    }
                                  }
                                }
                              }
                            }
                            """;

        // Act
        var output = Execute(json);

        // Assert
        Assert.Equal(
            $"[x; 1; 2]{Environment.NewLine}Node(1, value){Environment.NewLine}",
            output
        );
    }

    [Fact]
    public void NestedArrayElementCanBeRead()
    {
        // Arrange
        const string json = """
                            {
                              "seq": {
                                "left": {
                                  "assn": {
                                    "dst": "array",
                                    "src": {
                                      "array": [
                                        { "const": 1 },
                                        { "array": [{ "const": 2 }] }
                                      ]
                                    }
                                  }
                                },
                                "right": {
                                  "write": {
                                    "index": {
                                      "array": {
                                        "index": {
                                          "array": { "var": "array" },
                                          "index": { "const": 1 }
                                        }
                                      },
                                      "index": { "const": 0 }
                                    }
                                  }
                                }
                              }
                            }
                            """;

        // Act
        var output = Execute(json);

        // Assert
        Assert.Equal($"2{Environment.NewLine}", output);
    }

    [Fact]
    public void ArrayElementCanBeAssigned()
    {
        // Arrange
        const string json = """
                            {
                              "seq": {
                                "left": {
                                  "assn": {
                                    "dst": "array",
                                    "src": {
                                      "array": [{ "const": 1 }, { "const": 2 }]
                                    }
                                  }
                                },
                                "right": {
                                  "seq": {
                                    "left": {
                                      "assn": {
                                        "dst": {
                                          "index": {
                                            "array": { "var": "array" },
                                            "index": { "const": 1 }
                                          }
                                        },
                                        "src": { "const": 42 }
                                      }
                                    },
                                    "right": {
                                      "write": {
                                        "index": {
                                          "array": { "var": "array" },
                                          "index": { "const": 1 }
                                        }
                                      }
                                    }
                                  }
                                }
                              }
                            }
                            """;

        // Act
        var output = Execute(json);

        // Assert
        Assert.Equal($"42{Environment.NewLine}", output);
    }

    [Fact]
    public void ArrayLengthWritesElementCount()
    {
        // Arrange
        const string json = """
                            {
                              "write": {
                                "length": {
                                  "array": [
                                    { "const": 1 },
                                    { "const": 2 },
                                    { "const": 3 }
                                  ]
                                }
                              }
                            }
                            """;

        // Act
        var output = Execute(json);

        // Assert
        Assert.Equal($"3{Environment.NewLine}", output);
    }

    [Fact]
    public void ArrayIsWrittenInSourceLanguageFormat()
    {
        // Arrange
        const string json = """
                            {
                              "write": {
                                "array": [{ "const": 1 }, { "const": 2 }]
                              }
                            }
                            """;

        // Act
        var output = Execute(json);

        // Assert
        Assert.Equal($"[1; 2]{Environment.NewLine}", output);
    }

    [Fact]
    public void NestedArraySumProgramOutputs21()
    {
        // Arrange
        var programPath = Path.GetFullPath(
            Path.Combine(
                AppContext.BaseDirectory,
                "../../../../Interpreter/program.json"
            )
        );

        var json = File.ReadAllText(programPath);

        // Act
        var output = Execute(json);

        // Assert
        Assert.Equal($"21{Environment.NewLine}", output);
    }

    private static string Execute(
        string json,
        string input = "")
    {
        var binaryOperationParser = new BinaryOperationParser(
            new BinaryOperationChainFactory()
        );

        var expressionParser = new ExpressionParser(
            new ExpressionChainFactory(binaryOperationParser)
        );

        var statementParser = new StatementParser(
            new StatementChainFactory(expressionParser)
        );

        using JsonDocument document = JsonDocument.Parse(json);

        var result = statementParser.Parse(document.RootElement);

        var success =
            Assert.IsType<StatementParseResult.Success>(result);

        using var reader = new StringReader(input);
        using var writer = new StringWriter();

        var context = new InterpreterContext(
            reader,
            writer
        );

        success.Statement.Execute(context);

        return writer.ToString();
    }
}
