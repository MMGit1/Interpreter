using VirtualMachine.Contexts;
using VirtualMachine.Parsers;
using VirtualMachine.Parsers.Results;
using Machine = VirtualMachine.Machines.VirtualMachine;

if (args.Length != 1)
{
    Console.Error.WriteLine("Usage: VirtualMachine <program file>");
    return;
}

using var programFile = new StreamReader(args[0]);

var parser = new ProgramParser(new CommandChainFactory());
var result = parser.Parse(programFile);

switch (result)
{
    case ProgramParsingResult.Failure failure:
        Console.Error.WriteLine(failure.ErrorMessage);
        break;

    case ProgramParsingResult.Success success:
        var context = new Context(Console.In, Console.Out);
        var machine = new Machine();
        machine.Execute(success.Commands, context);
        break;
}
