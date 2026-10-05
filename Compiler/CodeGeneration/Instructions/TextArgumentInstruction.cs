using System.Text.Json;

namespace Compiler.CodeGeneration;

public sealed record TextArgumentInstruction(string Opcode, string Argument) : IVmInstruction
{
    public void WriteTo(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WriteString(Opcode, Argument);
        writer.WriteEndObject();
    }
}
