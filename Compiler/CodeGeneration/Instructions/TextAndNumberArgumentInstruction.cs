using System.Text.Json;

namespace Compiler.CodeGeneration;

public sealed record TextAndNumberArgumentInstruction(
    string Opcode,
    string TextArgument,
    int NumberArgument) : IVmInstruction
{
    public void WriteTo(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName(Opcode);
        writer.WriteStartArray();
        writer.WriteStringValue(TextArgument);
        writer.WriteNumberValue(NumberArgument);
        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
