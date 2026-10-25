using System.Text.Json;
using VirtualMachine.Commands;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers;

public class ProgramParser(
    ICommandChainFactory chainFactory)
{
    public ProgramParsingResult Parse(TextReader input)
    {
        List<ICommand> commands = [];
        var chain = chainFactory.Create();

        JsonDocument document;

        try
        {
            document = JsonDocument.Parse(input.ReadToEnd());
        }
        catch (JsonException exception)
        {
            return new ProgramParsingResult.Failure(exception.Message);
        }

        using (document)
        {
            if (document.RootElement.ValueKind != JsonValueKind.Array)
            {
                return new ProgramParsingResult.Failure("Program must be a JSON array");
            }

            foreach (var instruction in document.RootElement.EnumerateArray())
            {
                var tokenResult = GetTokens(instruction);

                if (tokenResult is TokenResult.Failure tokenFailure)
                {
                    return new ProgramParsingResult.Failure(tokenFailure.ErrorMessage);
                }

                var tokens = ((TokenResult.Success)tokenResult).Tokens;
                var enumerator = ((IEnumerable<string>)tokens).GetEnumerator();
                enumerator.MoveNext();
                var commandParsingResult = chain.Parse(enumerator);

                switch (commandParsingResult)
                {
                    case CommandParsingResult.Success success:
                        commands.Add(success.Command);
                        break;
                    case CommandParsingResult.Failure failure:
                        return new ProgramParsingResult.Failure(failure.ErrorMessage);
                }
            }
        }

        return new ProgramParsingResult.Success(commands);
    }

    private static TokenResult GetTokens(JsonElement instruction)
    {
        if (instruction.ValueKind == JsonValueKind.String)
        {
            return new TokenResult.Success([instruction.GetString()!]);
        }

        if (instruction.ValueKind != JsonValueKind.Object)
        {
            return new TokenResult.Failure("Instruction must be a string or an object");
        }

        var properties = instruction.EnumerateObject().ToArray();

        if (properties.Length != 1)
        {
            return new TokenResult.Failure("Instruction object must contain one property");
        }

        var property = properties[0];
        var tokens = new List<string> { property.Name };

        if (property.Value.ValueKind == JsonValueKind.String)
        {
            tokens.Add(property.Value.GetString()!);

            return new TokenResult.Success(tokens.ToArray());
        }

        if (property.Value.ValueKind == JsonValueKind.Number)
        {
            tokens.Add(property.Value.GetRawText());

            return new TokenResult.Success(tokens.ToArray());
        }

        if (property.Value.ValueKind != JsonValueKind.Array)
        {
            return new TokenResult.Failure(
                "Instruction argument must be a string, number or array"
            );
        }

        foreach (var argument in property.Value.EnumerateArray())
        {
            if (argument.ValueKind is not (JsonValueKind.String or JsonValueKind.Number))
            {
                return new TokenResult.Failure(
                    "Instruction argument array must contain strings or numbers"
                );
            }

            if (argument.ValueKind == JsonValueKind.String)
            {
                tokens.Add(argument.GetString()!);
            }
            else
            {
                tokens.Add(argument.GetRawText());
            }
        }

        return new TokenResult.Success(tokens.ToArray());
    }
}
