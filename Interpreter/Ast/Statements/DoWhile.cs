using Interpreter.Ast.Expressions;
using Interpreter.Context;

namespace Interpreter.Ast.Statements;

public class DoWhile(
    IStatement body,
    IExpression condition) : IStatement
{
    public void Execute(InterpreterContext context)
    {
        do
        {
            body.Execute(context);
        }
        while (condition.Evaluate(context) != 0);
    }
}