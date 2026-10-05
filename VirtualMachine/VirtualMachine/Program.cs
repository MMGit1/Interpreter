using VirtualMachine.Contexts;
using VirtualMachine.Parsers;
using VirtualMachine.Parsers.Results;
using Machine = VirtualMachine.Machines.VirtualMachine;

namespace VirtualMachine;

public static class Program
{
    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: VirtualMachine <program file>");
            return 1;
        }

        using var programFile = new StreamReader(args[0]);
        var result = new ProgramParser(new CommandChainFactory()).Parse(programFile);
        if (result is ProgramParsingResult.Failure failure)
        {
            Console.Error.WriteLine(failure.ErrorMessage);
            return 1;
        }

        var commands = ((ProgramParsingResult.Success)result).Commands;
        new Machine().Execute(commands, new Context(Console.In, Console.Out));
        return 0;
    }
}
