using Interpreter.Context;
using Interpreter.Ast.Values;

namespace Interpreter.Ast.Expressions;

public interface IExpression
{
    IValue Evaluate(InterpreterContext context);
}
