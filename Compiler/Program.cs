using Compiler.CodeGeneration;
using Compiler.Composition;
using Compiler.Lexing;

namespace Compiler;

public static class Program
{
    public static int Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: Compiler <source file> <output file>");
            return 1;
        }

        var source = File.ReadAllText(args[0]);
        var compiler = new SourceCompiler(
            new Tokenizer(),
            new ParserFactory().Create(),
            new CodeGenerator());
        var result = compiler.Compile(source);

        if (result is CompilationResult.Failure failure)
        {
            Console.Error.WriteLine($"Compilation error: {failure.ErrorMessage}");

            return 1;
        }

        var program = ((CompilationResult.Success)result).Program;
        File.WriteAllText(args[1], program.ToJson());

        return 0;
    }
}
