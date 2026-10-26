using Interpreter.Context;
using Interpreter.Ast.Values;

namespace Interpreter.Ast.Expressions;

public class Variable : IExpression
{
    public string Name { get; }

    public Variable(string name)
    {
        Name = name;
    }

    public IValue Evaluate(InterpreterContext context)
    {
        return context.GetVariable(Name);
    }
}
