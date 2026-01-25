# Prompt para Detección de Code Smells

**Contexto de uso:** Este prompt analiza el código de PortalEmpleo y detecta code smells como Long Method, Large Class, etc.

**Prompt completo:**
```
@agent Analiza el código de PortalEmpleo y detecta code smells:

## Code Smells a Detectar
1. Long Method - Métodos > 30 líneas
2. Large Class - Clases > 500 líneas
3. Long Parameter List - > 4 parámetros
4. Primitive Obsession
5. Switch Statements extensos

```csharp
public class CodeSmellDetector
{
    public AnalysisResult AnalyzeProject(string projectPath)
    {
        var files = Directory.GetFiles(projectPath, "*.cs", SearchOption.AllDirectories);
        var smells = new List<CodeSmell>();
        
        foreach (var file in files)
        {
            var content = File.ReadAllText(file);
            var tree = CSharpSyntaxTree.ParseText(content);
            var root = tree.GetRoot();
            
            foreach (var method in root.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                var lineCount = method.Span.EndLine - method.Span.StartLine;
                if (lineCount > 30)
                {
                    smells.Add(new CodeSmell
                    {
                        Type = CodeSmellType.LongMethod,
                        File = file,
                        Line = method.Span.StartLine,
                        Message = $"Method '{method.Identifier.ValueText}' has {lineCount} lines",
                        Severity = lineCount > 50 ? Severity.High : Severity.Medium
                    });
                }
            }
            
            foreach (var cls in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
            {
                var lineCount = cls.Span.EndLine - cls.Span.StartLine;
                if (lineCount > 500)
                {
                    smells.Add(new CodeSmell
                    {
                        Type = CodeSmellType.LargeClass,
                        File = file,
                        Line = cls.Span.StartLine,
                        Message = $"Class '{cls.Identifier.ValueText}' has {lineCount} lines"
                    });
                }
            }
        }
        
        return new AnalysisResult { SmellsDetected = smells };
    }
}
```
```
