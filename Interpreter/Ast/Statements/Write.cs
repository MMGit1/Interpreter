using Interpreter.Ast.Expressions;
using Interpreter.Context;

namespace Interpreter.Ast.Statements;

public class Write(IExpression expression) : IStatement
{
    public void Execute(InterpreterContext context)
    {
        var value = expression.Evaluate(context);

        context.WriteOutput(value);
    }
}