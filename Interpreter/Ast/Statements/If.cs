using Interpreter.Ast.Expressions;
using Interpreter.Context;

namespace Interpreter.Ast.Statements;

public class If(
    IExpression condition,
    IStatement thenBranch,
    IStatement elseBranch) : IStatement
{
    public void Execute(InterpreterContext context)
    {
        if (condition.Evaluate(context) != 0)
            thenBranch.Execute(context);
        else
            elseBranch.Execute(context);
    }
}