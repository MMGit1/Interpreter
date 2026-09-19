using Interpreter.Parser.BinaryOperations.Links;

namespace Interpreter.Parser.BinaryOperations;

public interface IBinaryOperationChainFactory
{
    IBinaryOperationParserLink Create();
}