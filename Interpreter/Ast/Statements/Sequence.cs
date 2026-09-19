using Interpreter.Context;

namespace Interpreter.Ast.Statements;

public class Sequence(
    IStatement left,
    IStatement right) : IStatement
{
    public void Execute(InterpreterContext context)
    {
        left.Execute(context);
        right.Execute(context);
    }
}