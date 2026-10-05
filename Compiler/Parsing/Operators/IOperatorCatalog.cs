namespace Compiler.Parsing.Operators;

public interface IOperatorCatalog
{
    bool TryGetBinary(string symbol, out IBinaryOperator operation);
    bool TryGetAssignment(string symbol, out IAssignmentOperator operation);
}
