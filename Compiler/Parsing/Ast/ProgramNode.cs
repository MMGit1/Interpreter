using Compiler.Parsing.Ast.Statements;

namespace Compiler.Parsing.Ast;

public sealed record ProgramNode(IReadOnlyList<IStatementNode> Statements);
