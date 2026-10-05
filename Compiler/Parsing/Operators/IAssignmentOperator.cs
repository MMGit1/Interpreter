namespace Compiler.Parsing.Operators;

public interface IAssignmentOperator
{
    string Symbol { get; }
    IBinaryOperator? UnderlyingBinaryOperator { get; }
}
