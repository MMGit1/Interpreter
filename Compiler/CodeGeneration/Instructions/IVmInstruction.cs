using System.Text.Json;

namespace Compiler.CodeGeneration;

public interface IVmInstruction
{
    void WriteTo(Utf8JsonWriter writer);
}
