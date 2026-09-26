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
                              READ
                              ST a
                              LD a
                              CONST 2
                              BINOP +
                              WRITE
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
    public void ParseSkipsBlankLinesAndWhitespace()
    {
        // Arrange
        const string source = "\n\tCONST\t5\n\nWRITE";

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
        const string source = "UNKNOWN";

        // Act
        var result = Parse(source);

        // Assert
        var failure = Assert.IsType<ProgramParsingResult.Failure>(result);
        Assert.Equal("Unknown expression", failure.ErrorMessage);
    }

    [Fact]
    public void ParseReturnsFailureWhenBinaryOperatorIsMissing()
    {
        // Arrange
        const string source = "BINOP";

        // Act
        var result = Parse(source);

        // Assert
        var failure = Assert.IsType<ProgramParsingResult.Failure>(result);
        Assert.Equal("BINOP requires an operator", failure.ErrorMessage);
    }

    private static ProgramParsingResult Parse(string source)
    {
        var parser = new ProgramParser(new CommandChainFactory());
        return parser.Parse(new StringReader(source));
    }
}
