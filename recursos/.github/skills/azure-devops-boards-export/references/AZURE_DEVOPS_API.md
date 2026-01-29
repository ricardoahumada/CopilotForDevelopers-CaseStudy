# Azure DevOps REST API Reference

## Authentication

### Personal Access Token (PAT)

Personal Access Tokens are the recommended authentication method for REST APIs.

```bash
# Generate Base64 encoding for Basic Auth
# Format: ":PAT"
echo -n ":YOUR_PAT_TOKEN" | base64

# Windows (PowerShell)
[Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes(":YOUR_PAT_TOKEN"))
```

### Required Headers

```
Authorization: Basic BASE64_PAT
Content-Type: application/json-patch+json
```

### PAT Scopes Required

| Scope | Description |
|-------|-------------|
| `vso.work_write` | Create and update work items |
| `vso.project_read` | Read project information |

## Work Items API

### Create Work Item

Creates a new work item in Azure DevOps Boards.

```http
POST https://dev.azure.com/{organization}/{project}/_apis/wit/workitems/${type}?api-version=7.1
```

#### URI Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| organization | string | Azure DevOps organization name |
| project | string | Project name or ID |
| type | string | Work item type (Task, User Story, Bug, etc.) |
| api-version | string | API version (7.1, 7.0, 6.0) |

#### Request Body

Uses JSON Patch format (RFC 6902):

```json
[
  { "op": "add", "path": "/fields/System.Title", "value": "Task title" },
  { "op": "add", "path": "/fields/System.Description", "value": "Description" },
  { "op": "add", "path": "/fields/Microsoft.VSTS.Common.Priority", "value": 2 }
]
```

#### Operation Types

| Operation | Description |
|-----------|-------------|
| `add` | Add a new field value |
| `replace` | Replace existing field value |
| `remove` | Remove a field value |

#### Example Request

```bash
curl -X POST \
  -H "Authorization: Basic $(echo -n ':PAT' | base64)" \
  -H "Content-Type: application/json-patch+json" \
  -d '[{"op":"add","path":"/fields/System.Title","value":"Sample task"}]' \
  "https://dev.azure.com/myorg/myproject/_apis/wit/workitems/Task?api-version=7.1"
```

### Get Work Item

Retrieves an existing work item.

```http
GET https://dev.azure.com/{organization}/{project}/_apis/wit/workitems/{id}?api-version=7.1
```

### Update Work Item

Updates an existing work item.

```http
PATCH https://dev.azure.com/{organization}/{project}/_apis/wit/workitems/{id}?api-version=7.1
```

### Query Work Items

Search for work items using WiQL (Work Item Query Language).

```http
POST https://dev.azure.com/{organization}/{project}/_apis/wit/wiql?api-version=7.1

Body:
{
  "query": "SELECT [System.Id] FROM WorkItems WHERE [TAINS 'TaskSystem.Title] CON' AND [System.State] = 'New'"
}
```

## Common Status Codes

| Code | Description |
|------|-------------|
| 200 | Successful operation |
| 201 | Created successfully |
| 400 | Bad request - invalid format |
| 401 | Unauthorized - invalid or missing PAT |
| 403 | Forbidden - insufficient permissions |
| 404 | Not found - project or work item doesn't exist |

## Rate Limiting

Azure DevOps enforces rate limits on API requests. For most scenarios:
- Up to 180 requests per minute for authenticated users
- Up to 60 requests per minute for anonymous users

Implement retry logic with exponential backoff for 429 (Too Many Requests) responses.

## Related Documentation

- [Work Items API Reference](https://learn.microsoft.com/en-us/rest/api/azure/devops/wit/)
- [Authentication Guidance](https://learn.microsoft.com/en-us/azure/devops/integrate/get-started/authentication/authentication-guidance)
- [JSON Patch Format](https://tools.ietf.org/html/rfc6902)
