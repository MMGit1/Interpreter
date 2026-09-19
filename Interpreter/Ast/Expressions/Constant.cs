using Interpreter.Context;

namespace Interpreter.Ast.Expressions;

public class Constant : IExpression
{
    public int Value { get; }
    
    public Constant(int value)
    {
        Value = value;
    }

    public int Evaluate(InterpreterContext context)
    {
        return Value;
    }
}