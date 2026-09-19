using Interpreter.Context;

namespace Interpreter.Ast.Statements;

public interface IStatement
{
    void Execute(InterpreterContext context);
}