using Compiler.Parsing;
using Compiler.Parsing.Ast;
using Compiler.Parsing.Ast.Expressions;
using Compiler.Parsing.Ast.Statements;

namespace Compiler.CodeGeneration;

public sealed class CodeGenerator : ICodeGenerator, IStatementVisitor, IExpressionVisitor
{
    private readonly List<IVmInstruction> _instructions = [];
    private int _labelNumber;
    private int _temporaryNumber;

    public CompilationResult Generate(ProgramNode program)
    {
        _instructions.Clear();
        _labelNumber = 0;
        _temporaryNumber = 0;

        foreach (var statement in program.Statements)
        {
            statement.Accept(this);
        }

        return new CompilationResult.Success(new VmProgram(_instructions.ToArray()));
    }

    public void Visit(ReadNode node)
    {
        Emit("READ");
        Emit("ST", node.Name);
    }

    public void Visit(WriteNode node)
    {
        node.Expression.Accept(this);
        Emit("WRITE");
    }

    public void Visit(AssignmentNode node)
    {
        if (node.Operator.UnderlyingBinaryOperator is not null)
        {
            Emit("LD", node.Name);
        }

        node.Expression.Accept(this);

        if (node.Operator.UnderlyingBinaryOperator is { } operation)
        {
            Emit("BINOP", operation.Symbol);
        }

        Emit("ST", node.Name);
    }

    public void Visit(ElementAssignmentNode node)
    {
        if (node.Operator.UnderlyingBinaryOperator is null)
        {
            EmitElementAssignment(node.Destination, node.Expression);

            return;
        }

        node.Destination.Accept(this);
        node.Expression.Accept(this);
        Emit("BINOP", node.Operator.UnderlyingBinaryOperator.Symbol);

        var temporary = NewTemporary();
        Emit("ST", temporary);
        EmitElementAssignment(node.Destination, new VariableNode(temporary));
    }

    public void Visit(BlockNode node)
    {
        foreach (var statement in node.Statements)
        {
            statement.Accept(this);
        }
    }

    public void Visit(IfNode node)
    {
        var elseLabel = NewLabel("else");
        node.Condition.Accept(this);
        Emit("JZ", elseLabel);
        node.Then.Accept(this);

        if (node.Else is null)
        {
            EmitLabel(elseLabel);
            return;
        }

        var endLabel = NewLabel("end");
        Emit("JMP", endLabel);
        EmitLabel(elseLabel);
        node.Else.Accept(this);
        EmitLabel(endLabel);
    }
    public void Visit(WhileNode node)
    {
        var conditionLabel = NewLabel("while_condition");
        var bodyLabel = NewLabel("while_body");
        Emit("JMP", conditionLabel);
        EmitLabel(bodyLabel);
        node.Body.Accept(this);
        EmitLabel(conditionLabel);
        node.Condition.Accept(this);
        Emit("JNZ", bodyLabel);
    }
    public void Visit(DoWhileNode node)
    {
        var bodyLabel = NewLabel("do_body");
        EmitLabel(bodyLabel);
        node.Body.Accept(this);
        node.Condition.Accept(this);
        Emit("JNZ", bodyLabel);
    }
    public void Visit(ForNode node)
    {
        node.Initialization.Accept(this);
        var conditionLabel = NewLabel("for_condition");
        var bodyLabel = NewLabel("for_body");
        Emit("JMP", conditionLabel);
        EmitLabel(bodyLabel);
        node.Body.Accept(this);
        node.Update.Accept(this);
        EmitLabel(conditionLabel);
        node.Condition.Accept(this);
        Emit("JNZ", bodyLabel);
    }

    public void Visit(SkipNode node)
    {
    }

    public void Visit(ConstantNode node)
    {
        Emit("CONST", node.Value);
    }

    public void Visit(StringNode node)
    {
        Emit("STRING", node.Value);
    }

    public void Visit(ArrayNode node)
    {
        foreach (var element in node.Elements)
        {
            element.Accept(this);
        }

        Emit("ARRAY", node.Elements.Count);
    }

    public void Visit(ElementNode node)
    {
        node.Context.Accept(this);
        node.Index.Accept(this);
        Emit("ELEM");
    }

    public void Visit(LengthNode node)
    {
        node.Context.Accept(this);
        Emit("LENGTH");
    }

    public void Visit(SExpressionNode node)
    {
        foreach (var argument in node.Arguments)
        {
            argument.Accept(this);
        }

        EmitSExpression(node.Name, node.Arguments.Count);
    }

    public void Visit(VariableNode node)
    {
        Emit("LD", node.Name);
    }

    public void Visit(BinaryNode node)
    {
        node.Left.Accept(this);
        node.Right.Accept(this);
        Emit("BINOP", node.Operator.Symbol);
    }

    private void Emit(string opcode)
    {
        _instructions.Add(new SimpleInstruction(opcode));
    }

    private void Emit(string opcode, string argument)
    {
        _instructions.Add(new TextArgumentInstruction(opcode, argument));
    }

    private void Emit(string opcode, int argument)
    {
        _instructions.Add(new NumberArgumentInstruction(opcode, argument));
    }

    private void EmitSExpression(string name, int argumentCount)
    {
        _instructions.Add(
            new TextAndNumberArgumentInstruction("SEXP", name, argumentCount)
        );
    }

    private void EmitLabel(string label)
    {
        Emit("LABEL", label);
    }

    private string NewLabel(string purpose)
    {
        return $"L_{purpose}_{_labelNumber++}";
    }

    private void EmitElementAssignment(
        ElementNode destination,
        IExpressionNode expression)
    {
        destination.Context.Accept(this);
        destination.Index.Accept(this);
        expression.Accept(this);
        Emit("SET");
    }

    private string NewTemporary()
    {
        return $"_temporary_{_temporaryNumber++}";
    }
}
