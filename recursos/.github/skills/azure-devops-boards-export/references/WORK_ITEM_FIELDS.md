# Azure DevOps Work Item Fields Reference

## System Fields

### Identification

| Field | Reference Name | Type | Description |
|-------|----------------|------|-------------|
| ID | System.Id | Integer | Unique identifier |
| Title | System.Title | String | Work item title (required) |
| Description | System.Description | HTML | Detailed description |
| Created Date | System.CreatedDate | DateTime | When created |
| Changed Date | System.ChangedDate | DateTime | Last modified date |
| Created By | System.CreatedBy | Identity | Who created it |
| Changed By | System.ChangedBy | Identity | Last modifier |

### State Management

| Field | Reference Name | Values |
|-------|----------------|--------|
| State | System.State | New, Active, Resolved, Closed |
| Reason | System.Reason | Why in current state |

### Area and Iteration

| Field | Reference Name | Description |
|-------|----------------|-------------|
| Area Path | System.AreaPath | Project area |
| Iteration Path | System.IterationPath | Sprint/iteration |
| Team Project | System.TeamProject | Parent project |

## Common Process Fields

### Priority

| Field | Reference Name | Values |
|-------|----------------|--------|
| Priority | Microsoft.VSTS.Common.Priority | 1 (High), 2 (Medium), 3 (Low) |

### Scheduling

| Field | Reference Name | Type | Description |
|-------|----------------|------|-------------|
| Remaining Work | Microsoft.VSTS.Scheduling.RemainingWork | Double | Hours remaining |
| Completed Work | Microsoft.VSTS.Scheduling.CompletedWork | Double | Hours completed |
| Original Estimate | Microsoft.VSTS.Scheduling.OriginalEstimate | Double | Initial estimate |

### Severity

| Field | Reference Name | Values |
|-------|----------------|--------|
| Severity | Microsoft.VSTS.Common.Severity | 1 - Critical, 2 - High, 3 - Medium, 4 - Low |

## Tagging

| Field | Reference Name | Type | Description |
|-------|----------------|------|-------------|
| Tags | System.Tags | String | Comma-separated tags |

## Work Item Types

### Task Fields

| Field | Reference Name | Required |
|-------|----------------|----------|
| System.Title | Yes | |
| System.Description | No | |
| Microsoft.VSTS.Common.Priority | No | |
| Microsoft.VSTS.Scheduling.RemainingWork | No | |

### User Story Fields

| Field | Reference Name | Required |
|-------|----------------|----------|
| System.Title | Yes | |
| System.Description | No | |
| Acceptance Criteria | System.Description | No | (often in Description) |
| Value Area | Microsoft.VSTS.Common.ValueArea | Business or Architectural |

### Bug Fields

| Field | Reference Name | Required |
|-------|----------------|----------|
| System.Title | Yes | |
| System.Description | No | |
| Severity | Microsoft.VSTS.Common.Severity | No |
| Priority | Microsoft.VSTS.Common.Priority | No |
| Steps to Reproduce | Microsoft.VSTS.Common.ReproSteps | No |

## Example: Creating a Task

```json
[
  {
    "op": "add",
    "path": "/fields/System.Title",
    "value": "T001: Create entity model"
  },
  {
    "op": "add",
    "path": "/fields/System.Description",
    "value": "Create Attachment entity with Guid, TaskId properties"
  },
  {
    "op": "add",
    "path": "/fields/Microsoft.VSTS.Common.Priority",
    "value": 2
  },
  {
    "op": "add",
    "path": "/fields/Microsoft.VSTS.Scheduling.RemainingWork",
    "value": 4
  },
  {
    "op": "add",
    "path": "/fields/System.Tags",
    "value": "domain,backend"
  }
]
```

## Related Documentation

- [Work Item Field Reference](https://learn.microsoft.com/en-us/azure/devops/work/queries/query-by-field)
- [Process Customization](https://learn.microsoft.com/en-us/azure/devops/organizations/settings/work/customize-process)
