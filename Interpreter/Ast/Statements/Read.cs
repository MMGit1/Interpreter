using Interpreter.Context;

namespace Interpreter.Ast.Statements;

public class Read(string variableName) : IStatement
{
    public void Execute(InterpreterContext context)
    {
        var value = context.ReadInput();

        context.SetVariable(variableName, value);
    }
}