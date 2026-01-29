---
name: azure-devops-boards-export
description: |
  Export tasks to Azure DevOps Boards as work items (Tasks, User Stories, Bugs).
  Use when user mentions: Azure DevOps, Boards, Work Items, tasks, sprints,
  task export, or sync with Azure DevOps.
license: MIT
compatibility: Requires Node.js 18+, Azure DevOps PAT token, and network access to dev.azure.com
metadata:
  author: Course Instructor
  version: "1.0.0"
---

# Azure DevOps Boards Export Skill

This skill creates work items in Azure DevOps Boards from a task list.

## When to Use This Skill

- User asks to export tasks to Azure DevOps
- User mentions syncing with Boards
- User wants to create Tasks/Stories/Bugs from specifications
- User needs to migrate tasks to Azure DevOps

## Prerequisites

User must provide:
1. **Organization**: Azure DevOps organization name (e.g., `mycompany`)
2. **Project**: Project name or ID
3. **PAT Token**: Personal Access Token with `vso.work_write` scope
4. **Work Item Type**: `Task`, `User Story`, or `Bug`

## Input Format

The skill accepts tasks in Markdown or JSON format:

### Markdown Format (from tasks.md)

```markdown
| ID | Task | Description | Hours |
|----|------|-------------|-------|
| T001 | Create Attachment entity | Class with Guid, TaskId properties | 1 |
| T002 | Status enum | AttachmentStatus enum | 0.5 |
```

### JSON Format (structured)

```json
[
  {
    "id": "T001",
    "title": "Create Attachment entity",
    "description": "Attachment class with Guid, TaskId properties",
    "hours": 1,
    "priority": 2,
    "tags": ["domain", "backend"]
  }
]
```

## Azure DevOps API

### Create Work Items Endpoint

```
POST https://dev.azure.com/{organization}/{project}/_apis/wit/workitems/${type}?api-version=7.1
```

### Required Headers

```
Authorization: Basic BASE64_PAT
Content-Type: application/json-patch+json
```

### Body Format (JSON Patch)

```json
[
  { "op": "add", "path": "/fields/System.Title", "value": "Task title" },
  { "op": "add", "path": "/fields/System.Description", "value": "Description" },
  { "op": "add", "path": "/fields/Microsoft.VSTS.Common.Priority", "value": 2 }
]
```

## Script Usage

### From VS Code with GitHub Copilot Chat

```bash
@skill/azure-devops-boards-export Export tasks to Azure DevOps.

Organization: mycompany
Project: PortalEmpleo
Type: Task
Token: ${AZURE_DEVOPS_PAT}

Input: specs/001-attachments/tasks.md
```

### From Command Line

```bash
# Install dependencies
npm install

# Run export
node scripts/export-tasks.js \
  --org "mycompany" \
  --project "PortalEmpleo" \
  --type "Task" \
  --token "$AZURE_DEVOPS_PAT" \
  --input "specs/001-attachments/tasks.md"

# Dry run (no changes)
node scripts/export-tasks.js \
  --org "mycompany" \
  --project "PortalEmpleo" \
  --type "Task" \
  --token "$AZURE_DEVOPS_PAT" \
  --input "specs/001-attachments/tasks.md" \
  --dry-run
```

## Common Work Item Fields

| Field | Description | Example |
|-------|-------------|---------|
| System.Title | Task title | "T001: Create Attachment entity" |
| System.Description | Detailed description | Task description |
| Microsoft.VSTS.Common.Priority | Priority (1=High, 2=Medium, 3=Low) | 2 |
| System.Tags | Comma-separated tags | "domain,backend" |
| Microsoft.VSTS.Scheduling.RemainingWork | Estimated hours | 1 |

## Error Handling

| Error | Cause | Solution |
|-------|-------|----------|
| 401 Unauthorized | Invalid PAT or missing permissions | Verify token with `vso.work_write` scope |
| 404 Not Found | Incorrect project or organization | Verify names |
| 400 Bad Request | Invalid JSON format | Check body syntax |

## Example Output

```json
{
  "success": true,
  "created": [
    {
      "id": 131,
      "title": "T001: Create Attachment entity",
      "url": "https://dev.azure.com/mycompany/PortalEmpleo/_workitems/edit/131"
    }
  ],
  "failed": []
}
```

## Important Notes

- Local IDs (T001, T010) are included in title for traceability
- Tasks are created in "New" state
- Script prompts for confirmation before creating in production
- Results are saved to `azure-devops-results.json`
