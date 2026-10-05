using System.Buffers;
using System.Text;
using System.Text.Json;

namespace Compiler.CodeGeneration;

public sealed class VmProgram(IReadOnlyList<IVmInstruction> instructions)
{
    public string ToJson()
    {
        var buffer = new ArrayBufferWriter<byte>();
        using var writer = new Utf8JsonWriter(buffer, new JsonWriterOptions { Indented = true });

        writer.WriteStartArray();

        foreach (var instruction in instructions)
        {
            instruction.WriteTo(writer);
        }

        writer.WriteEndArray();
        writer.Flush();

        return Encoding.UTF8.GetString(buffer.WrittenSpan);
    }
}
