using System.Text.Json;

namespace Compiler.CodeGeneration;

public sealed record SimpleInstruction(string Opcode) : IVmInstruction
{
    public void WriteTo(Utf8JsonWriter writer) => writer.WriteStringValue(Opcode);
}
