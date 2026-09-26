using VirtualMachine.Commands;
using VirtualMachine.Parsers.Links;
using VirtualMachine.Parsers.Results;

namespace VirtualMachine.Parsers;

public class ProgramParser(
    ICommandChainFactory chainFactory)
{
    public ProgramParsingResult Parse(TextReader input)
    {
        List<ICommand> commands = [];

        var chain = chainFactory.Create();

        string? line;

        while ((line = input.ReadLine()) is not null)
        {
            var tokens = line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);

            var enumerator = tokens.AsEnumerable().GetEnumerator();
            
            if (!enumerator.MoveNext())
            {
                continue;
            }

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

        return new ProgramParsingResult.Success(commands);
    }
}