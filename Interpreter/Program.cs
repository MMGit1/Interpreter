using System.Text.Json;
using Interpreter.Context;
using Interpreter.Parser.BinaryOperations;
using Interpreter.Parser.Expressions;
using Interpreter.Parser.Results;
using Interpreter.Parser.Statements;

namespace Interpreter;

internal class Program
{
    private static void Main(string[] args)
    {
        var binaryOperationParser = new BinaryOperationParser(
            new BinaryOperationChainFactory()
        );

        var expressionParser = new ExpressionParser(
            new ExpressionChainFactory(binaryOperationParser)
        );

        var statementParser = new StatementParser(
            new StatementChainFactory(expressionParser)
        );

        var json = File.ReadAllText("program.json");

        using JsonDocument document = JsonDocument.Parse(json);

        var result = statementParser.Parse(document.RootElement);

        if (result is StatementParseResult.Success success)
        {
            var context = new InterpreterContext(
                Console.In,
                Console.Out
            );

            success.Statement.Execute(context);
        }
        else if (result is StatementParseResult.Failure failure)
        {
            Console.WriteLine(failure.Message);
        }
    }
}