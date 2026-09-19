using Interpreter.Context;

namespace Interpreter.Ast.Expressions;

public interface IExpression
{
    int Evaluate(InterpreterContext context);
}