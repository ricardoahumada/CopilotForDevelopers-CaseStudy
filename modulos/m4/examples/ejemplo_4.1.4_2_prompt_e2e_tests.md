# Prompt para E2E Tests

**Contexto de uso:** Este prompt genera tests E2E con Playwright para el flujo completo de gestión de tareas.

**Prompt completo:**
```
@test-agent Genera tests E2E con Playwright:

```typescript
test.describe('Tarea Management E2E', () => {
  test('Complete Tarea Workflow', async ({ page }) => {
    // Login
    await page.goto('/login');
    await page.fill('[data-testid=email]', 'test@portalempleo.com');
    await page.fill('[data-testid=password]', 'Test123!');
    await page.click('[data-testid=login-button]');
    await page.waitForURL('/dashboard');
    
    // Create
    await page.goto('/tareas');
    await page.click('[data-testid=create-tarea-button]');
    await page.fill('[data-testid=titulo-input]', 'E2E Test Task');
    await page.selectOption('[data-testid=prioridad-select]', 'alta');
    await page.click('[data-testid=submit-button]');
    await expect(page.locator('[data-testid=success-toast]')).toBeVisible();
    
    // Verify in list
    await expect(page.locator('[data-testid=tarea-card]', { hasText: 'E2E Test Task' })).toBeVisible();
  });
});
```
```
