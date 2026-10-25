using VirtualMachine.Commands;
using VirtualMachine.Parsers;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Tests;

public class ProgramParserTests
{
    [Fact]
    public void ParseBuildsCommandsForValidProgram()
    {
        // Arrange
        const string source = """
                              [
                                "READ",
                                { "ST": "a" },
                                { "LD": "a" },
                                { "CONST": 2 },
                                { "BINOP": "+" },
                                "WRITE"
                              ]
                              """;

        // Act
        var result = Parse(source);

        // Assert
        var success = Assert.IsType<ProgramParsingResult.Success>(result);
        Assert.Collection(
            success.Commands,
            command => Assert.IsType<Read>(command),
            command => Assert.IsType<Store>(command),
            command => Assert.IsType<Load>(command),
            command => Assert.IsType<Const>(command),
            command => Assert.IsType<BinaryOperationCommand>(command),
            command => Assert.IsType<Write>(command)
        );
    }

    [Fact]
    public void ParseAcceptsFormattedJson()
    {
        // Arrange
        const string source = """
                              [

                                { "CONST": 5 },

                                "WRITE"
                              ]
                              """;

        // Act
        var result = Parse(source);

        // Assert
        var success = Assert.IsType<ProgramParsingResult.Success>(result);
        Assert.Equal(2, success.Commands.Count);
    }

    [Fact]
    public void ParseReturnsFailureForUnknownCommand()
    {
        // Arrange
        const string source = "[\"UNKNOWN\"]";

        // Act
        var result = Parse(source);

        // Assert
        var failure = Assert.IsType<ProgramParsingResult.Failure>(result);
        Assert.Equal("Unknown expression", failure.ErrorMessage);
    }

    [Fact]
    public void ParseReturnsFailureWhenInstructionArgumentIsNotScalar()
    {
        // Arrange
        const string source = "[{ \"BINOP\": null }]";

        // Act
        var result = Parse(source);

        // Assert
        var failure = Assert.IsType<ProgramParsingResult.Failure>(result);
        Assert.Equal(
            "Instruction argument must be a string, number or array",
            failure.ErrorMessage
        );
    }

    [Fact]
    public void ParseAcceptsSExpressionWithTwoArguments()
    {
        // Arrange
        const string source = """
                              [
                                { "SEXP": ["Node", 2] }
                              ]
                              """;

        // Act
        var result = Parse(source);

        // Assert
        var success = Assert.IsType<ProgramParsingResult.Success>(result);
        Assert.IsType<SExpression>(success.Commands.Single());
    }

    private static ProgramParsingResult Parse(string source)
    {
        var parser = new ProgramParser(new CommandChainFactory());
        return parser.Parse(new StringReader(source));
    }
}
