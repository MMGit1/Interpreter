namespace Compiler.CodeGeneration;

public abstract record CompilationResult
{
    private CompilationResult()
    {
    }

    public sealed record Success(VmProgram Program) : CompilationResult;

    public sealed record Failure(string ErrorMessage) : CompilationResult;
}
