using Interpreter.Ast.Expressions;
using Interpreter.Ast.Values;
using Interpreter.Context;
using Interpreter.Exceptions;

namespace Interpreter.Ast.Statements;

public sealed class DoWhile(
    IStatement body,
    IExpression condition) : IStatement
{
    public void Execute(InterpreterContext context)
    {
        do
        {
            body.Execute(context);

            if (condition.Evaluate(context) is not IntegerValue value)
            {
                throw new InvalidValueTypeException("Integer value expected");
            }

            if (value.Value == 0)
            {
                return;
            }
        }
        while (true);
    }
}
