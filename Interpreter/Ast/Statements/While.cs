using Interpreter.Ast.Expressions;
using Interpreter.Ast.Values;
using Interpreter.Context;
using Interpreter.Exceptions;

namespace Interpreter.Ast.Statements;

public sealed class While(
    IExpression condition,
    IStatement body) : IStatement
{
    public void Execute(InterpreterContext context)
    {
        while (true)
        {
            if (condition.Evaluate(context) is not IntegerValue value)
            {
                throw new InvalidValueTypeException("Integer value expected");
            }

            if (value.Value == 0)
            {
                return;
            }

            body.Execute(context);
        }
    }
}
