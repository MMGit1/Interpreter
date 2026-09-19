using Interpreter.Ast.Expressions;
using Interpreter.Context;

namespace Interpreter.Ast.Statements;

public class While(
    IExpression condition,
    IStatement body) : IStatement
{
    public void Execute(InterpreterContext context)
    {
        while (condition.Evaluate(context) != 0)
        {
            body.Execute(context);
        }
    }
}