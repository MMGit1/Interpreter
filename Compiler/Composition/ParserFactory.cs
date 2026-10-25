using Compiler.Parsing;
using Compiler.Parsing.Operators;
using Compiler.Parsing.StatementParsing;

namespace Compiler.Composition;

public sealed class ParserFactory : IParserFactory
{
    public ISourceParser Create()
    {
        var binaryOperators = CreateBinaryOperators();
        var assignmentOperators = CreateAssignmentOperators(binaryOperators);
        var catalog = new OperatorCatalog(binaryOperators, assignmentOperators);

        return new SourceParser(catalog, new StatementParserChainFactory());
    }

    private static IReadOnlyList<IBinaryOperator> CreateBinaryOperators()
    {
        return
        [
            new BinaryOperator("!!"),
            new BinaryOperator("&&"),
            new BinaryOperator("=="),
            new BinaryOperator("!="),
            new BinaryOperator("<="),
            new BinaryOperator("<"),
            new BinaryOperator(">="),
            new BinaryOperator(">"),
            new BinaryOperator(":"),
            new BinaryOperator("+"),
            new BinaryOperator("-"),
            new BinaryOperator("*"),
            new BinaryOperator("/"),
            new BinaryOperator("%")
        ];
    }

    private static IReadOnlyList<IAssignmentOperator> CreateAssignmentOperators(
        IReadOnlyList<IBinaryOperator> binaryOperators)
    {
        var operatorsBySymbol = binaryOperators.ToDictionary(operation => operation.Symbol);

        return
        [
            new AssignmentOperator("=", null),
            new AssignmentOperator("+=", operatorsBySymbol["+"]),
            new AssignmentOperator("-=", operatorsBySymbol["-"]),
            new AssignmentOperator("*=", operatorsBySymbol["*"]),
            new AssignmentOperator("/=", operatorsBySymbol["/"]),
            new AssignmentOperator("%=", operatorsBySymbol["%"])
        ];
    }
}
