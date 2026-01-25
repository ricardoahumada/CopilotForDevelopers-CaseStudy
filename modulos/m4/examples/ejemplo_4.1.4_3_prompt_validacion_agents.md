# Prompt para Validación Contra AGENTS.md

**Contexto de uso:** Este prompt crea sistema de validación continua contra AGENTS.md.

**Prompt completo:**
```
@test-agent Crea sistema de validación continua contra AGENTS.md:

```csharp
public class AgentsMdComplianceValidator
{
    private readonly List<string> _violations = new();
    
    public ValidationResult ValidateProject()
    {
        var testFiles = Directory.GetFiles(".", "*.cs", SearchOption.AllDirectories)
            .Where(f => f.Contains("Tests"));
        
        foreach (var file in testFiles)
            ValidateFile(file);
        
        return new ValidationResult { IsCompliant = !_violations.Any(), Violations = _violations };
    }
    
    private void ValidateFile(string filePath)
    {
        var content = File.ReadAllText(filePath);
        
        if (content.Contains("Assert.") && !content.Contains("FluentAssertions"))
            _violations.Add($"{filePath}: Use FluentAssertions instead of Assert");
        
        if (Regex.IsMatch(content, @"public class \w+Test"))
            _violations.Add($"{filePath}: Test class should end with 'Tests'");
    }
}
```

```yaml
name: AGENTS.md Compliance
on:
  push:
    paths: ['tests/**/*.cs', 'AGENTS.md']
jobs:
  validate:
    runs-on: ubuntu-latest
    steps:
      - name: Validate Compliance
        run: dotnet run --project tools/Validators/ --agents-md AGENTS.md --tests tests/
      - name: Check Violations
        run: |
          VIOLATIONS=$(cat compliance-report.json | jq '.violations | length')
          if [ "$VIOLATIONS" -gt 0 ]; then exit 1; fi
```
```
