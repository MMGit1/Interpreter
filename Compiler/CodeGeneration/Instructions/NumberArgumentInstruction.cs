using System.Text.Json;

namespace Compiler.CodeGeneration;

public sealed record NumberArgumentInstruction(string Opcode, int Argument) : IVmInstruction
{
    public void WriteTo(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WriteNumber(Opcode, Argument);
        writer.WriteEndObject();
    }
}
