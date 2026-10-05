namespace Compiler.Parsing.Operators;

public sealed class OperatorCatalog(
    IEnumerable<IBinaryOperator> binaryOperators,
    IEnumerable<IAssignmentOperator> assignmentOperators) : IOperatorCatalog
{
    private readonly Dictionary<string, IBinaryOperator> _binaryOperators =
        binaryOperators.ToDictionary(operation => operation.Symbol);

    private readonly Dictionary<string, IAssignmentOperator> _assignmentOperators =
        assignmentOperators.ToDictionary(operation => operation.Symbol);

    public bool TryGetBinary(string symbol, out IBinaryOperator operation) =>
        _binaryOperators.TryGetValue(symbol, out operation!);

    public bool TryGetAssignment(string symbol, out IAssignmentOperator operation) =>
        _assignmentOperators.TryGetValue(symbol, out operation!);
}
