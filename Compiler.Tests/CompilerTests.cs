using Compiler.CodeGeneration;
using Compiler.Composition;
using Compiler.Lexing;
using VirtualMachine.Contexts;
using VirtualMachine.Parsers;
using VirtualMachine.Parsers.Results;
using Machine = VirtualMachine.Machines.VirtualMachine;

namespace Compiler.Tests;

public class CompilerTests
{
    [Fact]
    public void CompileProducesProgramThatVirtualMachineExecutes()
    {
        // Arrange
        const string source = """
                              {
                                  read(n);
                                  sum = 0;
                                  while (n > 0) {
                                      sum += n;
                                      n = n - 1;
                                  }
                                  write(sum);
                              }
                              """;

        // Act
        var output = CompileAndExecute(source, "4");

        // Assert
        AssertSingleLine("10", output);
    }

    [Fact]
    public void CompileSupportsIfElifElseAndFor()
    {
        // Arrange
        const string source = """
                              {
                                  read(x);
                                  if (x < 0) write(1);
                                  elif (x == 0) write(2);
                                  else {
                                      for (i = 0; i < x; i += 1) write(i);
                                  }
                              }
                              """;

        // Act
        var output = CompileAndExecute(source, "3");

        // Assert
        Assert.Equal(["0", "1", "2"], ReadLines(output));
    }

    [Fact]
    public void CompileSupportsDoWhileAndCompoundAssignments()
    {
        // Arrange
        const string source = """
                              {
                                  value = 20;
                                  value -= 4;
                                  value /= 2;
                                  value *= 3;
                                  value %= 7;
                                  do {
                                      value += 1;
                                  } while (value < 4);
                                  write(value);
                              }
                              """;

        // Act
        var output = CompileAndExecute(source);

        // Assert
        AssertSingleLine("4", output);
    }

    [Fact]
    public void CompileSupportsCommentsAndLogicalOperators()
    {
        // Arrange
        const string source = """
                              {
                                  read(value);
                                  if ((value >= 0) && (value <= 10)) {
                                      write(1);
                                  }
                                  else {
                                      write(0);
                                  }

                                  if ((value == 0) !! (value > 10)) {
                                      write(2);
                                  }
                                  else {
                                      write(3);
                                  }
                              }
                              """;

        // Act
        var output = CompileAndExecute(source, "0");

        // Assert
        Assert.Equal(["1", "2"], ReadLines(output));
    }

    [Fact]
    public void CompileKeepsNestedExpressionPrecedence()
    {
        // Arrange
        const string source = """
                              {
                                  result = 2 + 3 * (4 + 1);
                                  write(result);
                              }
                              """;

        // Act
        var output = CompileAndExecute(source);

        // Assert
        AssertSingleLine("17", output);
    }

    [Fact]
    public void CompileReturnsFailureForInvalidSource()
    {
        // Arrange
        const string source = "{ write(1); ";
        var compiler = CreateCompiler();

        // Act
        var result = compiler.Compile(source);

        // Assert
        Assert.IsType<CompilationResult.Failure>(result);
    }

    private static global::Compiler.SourceCompiler CreateCompiler()
    {
        return new global::Compiler.SourceCompiler(
            new Tokenizer(),
            new ParserFactory().Create(),
            new CodeGenerator());
    }

    private static string CompileAndExecute(string source, string input = "")
    {
        var compilation = CreateCompiler().Compile(source);
        var compiledProgram = Assert.IsType<CompilationResult.Success>(compilation).Program;
        var vmProgram = new ProgramParser(new CommandChainFactory()).Parse(new StringReader(compiledProgram.ToJson()));
        var commands = Assert.IsType<ProgramParsingResult.Success>(vmProgram).Commands;
        using var output = new StringWriter();

        new Machine().Execute(commands, new Context(new StringReader(input), output));

        return output.ToString();
    }

    private static void AssertSingleLine(string expected, string output)
    {
        var lines = ReadLines(output);
        Assert.Equal([expected], lines);
    }

    private static string[] ReadLines(string output)
    {
        return output.Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries);
    }
}
