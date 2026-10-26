using Interpreter.Context;
using Interpreter.Ast.Values;

namespace Interpreter.Ast.Expressions;

public class Constant : IExpression
{
    public int Value { get; }
    
    public Constant(int value)
    {
        Value = value;
    }

    public IValue Evaluate(InterpreterContext context)
    {
        return new IntegerValue(Value);
    }
}
