using Interpreter.Context;

namespace Interpreter.Ast.Expressions;

public class Variable : IExpression
{
    public string Name { get; }

    public Variable(string name)
    {
        Name = name;
    }

    public int Evaluate(InterpreterContext context)
    {
        return context.GetVariable(Name);
    }
}