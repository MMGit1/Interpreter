namespace Compiler.Parsing.Operators;

public sealed record AssignmentOperator(string Symbol, IBinaryOperator? UnderlyingBinaryOperator) : IAssignmentOperator;
