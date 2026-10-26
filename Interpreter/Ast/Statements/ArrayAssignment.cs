using Interpreter.Ast.Expressions;
using Interpreter.Context;

namespace Interpreter.Ast.Statements;

public sealed class ArrayAssignment(ArrayElementAccess destination, IExpression source) : IStatement
{
    public void Execute(InterpreterContext context)
    {
        destination.SetValue(context, source.Evaluate(context));
    }
}
