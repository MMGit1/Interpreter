using VirtualMachine.Contexts;
using VirtualMachine.Parsers;
using VirtualMachine.Parsers.Results;
using Machine = VirtualMachine.Machines.VirtualMachine;

namespace VirtualMachine.Tests;

public class ProgramExecutionTests
{
    [Theory]
    [InlineData("+", 2, 3, 5)]
    [InlineData("-", 10, 3, 7)]
    [InlineData("*", 4, 5, 20)]
    [InlineData("/", 10, 2, 5)]
    [InlineData("%", 10, 3, 1)]
    [InlineData("==", 5, 5, 1)]
    [InlineData("!=", 5, 5, 0)]
    [InlineData("<", 2, 3, 1)]
    [InlineData("<=", 3, 3, 1)]
    [InlineData(">", 3, 2, 1)]
    [InlineData(">=", 2, 3, 0)]
    [InlineData("&&", 1, 0, 0)]
    [InlineData("!!", 0, 5, 1)]
    public void ExecuteParsesAndEvaluatesBinaryOperation(
        string operation,
        int left,
        int right,
        int expected)
    {
        // Arrange
        var source = $$"""
                         [
                           { "CONST": {{left}} },
                           { "CONST": {{right}} },
                           { "BINOP": "{{operation}}" },
                           "WRITE"
                         ]
                         """;

        // Act
        var output = Execute(source);

        // Assert
        AssertSingleLine(expected.ToString(), output);
    }

    [Fact]
    public void ExecuteReadsStoresLoadsAndWritesValue()
    {
        // Arrange
        const string source = """
                              [
                                "READ",
                                { "ST": "number" },
                                { "LD": "number" },
                                "WRITE"
                              ]
                              """;

        // Act
        var output = Execute(
            source,
            "42"
        );

        // Assert
        AssertSingleLine("42", output);
    }

    [Fact]
    public void ExecuteJumpCanTargetLaterLabel()
    {
        // Arrange
        const string source = """
                              [
                                { "JMP": "end" },
                                { "CONST": 99 },
                                "WRITE",
                                { "LABEL": "end" },
                                { "CONST": 7 },
                                "WRITE"
                              ]
                              """;

        // Act
        var output = Execute(
            source
        );

        // Assert
        AssertSingleLine("7", output);
    }

    [Fact]
    public void ExecuteJumpIfZeroSkipsCommandsWhenConditionIsZero()
    {
        // Arrange
        const string source = """
                              [
                                { "CONST": 0 },
                                { "JZ": "end" },
                                { "CONST": 99 },
                                "WRITE",
                                { "LABEL": "end" },
                                { "CONST": 1 },
                                "WRITE"
                              ]
                              """;

        // Act
        var output = Execute(
            source
        );

        // Assert
        AssertSingleLine("1", output);
    }

    [Fact]
    public void ExecuteJumpIfNotZeroContinuesWhenConditionIsZero()
    {
        // Arrange
        const string source = """
                              [
                                { "CONST": 0 },
                                { "JNZ": "skip" },
                                { "CONST": 5 },
                                "WRITE",
                                { "LABEL": "skip" }
                              ]
                              """;

        // Act
        var output = Execute(
            source
        );

        // Assert
        AssertSingleLine("5", output);
    }

    [Fact]
    public void ExecuteBuildsReadsAndChangesArray()
    {
        // Arrange
        const string source = """
                              [
                                { "CONST": 10 },
                                { "CONST": 20 },
                                { "ARRAY": 2 },
                                { "ST": "values" },
                                { "LD": "values" },
                                { "CONST": 1 },
                                { "CONST": 42 },
                                "SET",
                                { "LD": "values" },
                                { "CONST": 1 },
                                "ELEM",
                                "WRITE",
                                { "LD": "values" },
                                "LENGTH",
                                "WRITE"
                              ]
                              """;

        // Act
        var output = Execute(source);

        // Assert
        Assert.Equal($"42{Environment.NewLine}2{Environment.NewLine}", output);
    }

    [Fact]
    public void ExecuteWritesStringAndSExpression()
    {
        // Arrange
        const string source = """
                              [
                                { "STRING": "hello" },
                                "WRITE",
                                { "CONST": 1 },
                                { "STRING": "x" },
                                { "SEXP": ["Node", 2] },
                                "WRITE"
                              ]
                              """;

        // Act
        var output = Execute(source);

        // Assert
        Assert.Equal(
            $"hello{Environment.NewLine}Node(1, x){Environment.NewLine}",
            output
        );
    }

    private static string Execute(string source, string input = "")
    {
        using var inputReader = new StringReader(input);
        using var outputWriter = new StringWriter();

        var parser = new ProgramParser(new CommandChainFactory());
        var result = parser.Parse(new StringReader(source));
        var success = Assert.IsType<ProgramParsingResult.Success>(result);
        var context = new Context(inputReader, outputWriter);
        new Machine().Execute(success.Commands, context);

        return outputWriter.ToString();
    }

    private static void AssertSingleLine(string expected, string output)
    {
        using var reader = new StringReader(output);

        Assert.Equal(expected, reader.ReadLine());
        Assert.Null(reader.ReadLine());
    }
}
