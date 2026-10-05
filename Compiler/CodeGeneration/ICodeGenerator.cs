using Compiler.Parsing;
using Compiler.Parsing.Ast;

namespace Compiler.CodeGeneration;

public interface ICodeGenerator
{
    CompilationResult Generate(ProgramNode program);
}
