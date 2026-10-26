using Interpreter.Ast.Expressions;
using Interpreter.Ast.Values;
using Interpreter.Context;
using Interpreter.Exceptions;

namespace Interpreter.Ast.Statements;

public sealed class If(
    IExpression condition,
    IStatement thenBranch,
    IStatement elseBranch) : IStatement
{
    public void Execute(InterpreterContext context)
    {
        if (condition.Evaluate(context) is not IntegerValue value)
        {
            throw new InvalidValueTypeException("Integer value expected");
        }

        if (value.Value != 0)
        {
            thenBranch.Execute(context);

            return;
        }

        elseBranch.Execute(context);
    }
}
