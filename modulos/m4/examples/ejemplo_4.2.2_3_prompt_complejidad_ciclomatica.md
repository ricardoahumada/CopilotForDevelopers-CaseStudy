# Prompt para Análisis de Complejidad

**Contexto de uso:** Este prompt analiza complejidad ciclomática del código.

**Prompt completo:**
```
@agent Analiza complejidad ciclomática:

## Métricas
- Complejidad 1-10: Código simple
- Complejidad 11-20: Moderado
- Complejidad 21-50: Complejo
- Complejidad > 50: Muy alto riesgo

```csharp
public class ComplexityAnalyzer
{
    public ComplexityReport Analyze(string projectPath)
    {
        var methods = new List<MethodComplexity>();
        var files = Directory.GetFiles(projectPath, "*.cs");
        
        foreach (var file in files)
        {
            var content = File.ReadAllText(file);
            var tree = CSharpSyntaxTree.ParseText(content);
            var root = tree.GetRoot();
            
            foreach (var method in root.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                var complexity = 1;
                complexity += method.DescendantNodes().Count(n =>
                    n is IfStatementSyntax or SwitchStatementSyntax or 
                    ForStatementSyntax or WhileStatementSyntax or 
                    ConditionalExpressionSyntax);
                
                methods.Add(new MethodComplexity
                {
                    File = file,
                    MethodName = method.Identifier.ValueText,
                    Complexity = complexity,
                    LinesOfCode = method.Span.EndLine - method.Span.StartLine
                });
            }
        }
        
        return new ComplexityReport
        {
            MethodsAnalyzed = methods.Count,
            AverageComplexity = methods.Average(m => m.Complexity),
            HighComplexityMethods = methods.Where(m => m.Complexity > 20).ToList()
        };
    }
}
```
```
