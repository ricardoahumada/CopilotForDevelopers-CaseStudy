# Prompt para Detección de Duplicación

**Contexto de uso:** Este prompt detecta código duplicado en PortalEmpleo.

**Prompt completo:**
```
@agent Detecta código duplicado en PortalEmpleo:

```csharp
public class DuplicateCodeDetector
{
    public DuplicateReport FindDuplicates(string projectPath, int minLines = 6)
    {
        var files = GetCodeFiles(projectPath);
        var duplicates = new List<DuplicateBlock>();
        var blocks = new Dictionary<string, List<CodeBlock>>();
        
        foreach (var file in files)
        {
            var content = File.ReadAllText(file);
            var lines = content.Split('\n').ToList();
            
            for (int i = 0; i < lines.Count - minLines; i++)
            {
                var blockContent = string.Join('\n', lines.Skip(i).Take(minLines));
                var key = NormalizeBlock(blockContent);
                if (!blocks.ContainsKey(key)) blocks[key] = new List<CodeBlock>();
                blocks[key].Add(new CodeBlock { FilePath = file, Content = blockContent });
            }
        }
        
        foreach (var kvp in blocks.Where(k => k.Value.Count > 1))
        {
            duplicates.Add(new DuplicateBlock { Content = kvp.Value[0].Content, Occurrences = kvp.Value.Select(b => b.FilePath).ToList() });
        }
        
        return new DuplicateReport { Duplicates = duplicates };
    }
    
    private string NormalizeBlock(string content)
    {
        return Regex.Replace(content, @"\s+", " ").Replace("var ", "");
    }
}
```
```
