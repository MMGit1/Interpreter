using Interpreter.Ast.Expressions;
using Interpreter.Context;

namespace Interpreter.Ast.Statements;

public class Assignment(
    string destination,
    IExpression source) : IStatement
{
    public void Execute(InterpreterContext context)
    {
        var value = source.Evaluate(context);

        context.SetVariable(destination, value);
    }
}