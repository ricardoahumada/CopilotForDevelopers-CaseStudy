#!/usr/bin/env node

/**
 * Azure DevOps Boards Export Script
 *
 * Usage:
 *   node scripts/export-tasks.js --org ORG --project PROJECT --type TYPE --token TOKEN --input FILE
 *
 * Options:
 *   --org       Azure DevOps organization name
 *   --project   Project name or ID
 *   --type      Work item type (Task, User Story, Bug)
 *   --token     Personal Access Token
 *   --input     Input file (Markdown table or JSON)
 *   --dry-run   Show what would be created without creating
 */

const fs = require('fs');
const path = require('path');
const https = require('https');

const args = process.argv.slice(2).reduce((acc, arg, i) => {
  if (arg.startsWith('--')) {
    const key = arg.slice(2);
    acc[key] = process.argv[i + 3] || '';
  }
  return acc;
}, {});

async function parseInput(inputPath) {
  const content = fs.readFileSync(inputPath, 'utf-8');
  
  // Try JSON first
  try {
    return JSON.parse(content);
  } catch {
    // Parse Markdown table
    const lines = content.trim().split('\n');
    const tasks = [];
    
    // Skip header and separator rows
    for (let i = 2; i < lines.length; i++) {
      const cols = lines[i].split('|').slice(1, -1).map(c => c.trim());
      if (cols.length >= 3) {
        tasks.push({
          id: cols[0],
          title: `${cols[0]}: ${cols[1]}`,
          description: cols[2],
          hours: parseFloat(cols[3]) || 0
        });
      }
    }
    return tasks;
  }
}

function createWorkItem(org, project, type, token, task) {
  const operations = [
    { op: 'add', path: '/fields/System.Title', value: task.title },
    { op: 'add', path: '/fields/System.Description', value: task.description },
    { op: 'add', path: '/fields/Microsoft.VSTS.Common.Priority', value: task.priority || 2 }
  ];
  
  if (task.hours) {
    operations.push({ op: 'add', path: '/fields/Microsoft.VSTS.Scheduling.RemainingWork', value: task.hours });
  }
  
  if (task.tags) {
    operations.push({ op: 'add', path: '/fields/System.Tags', value: Array.isArray(task.tags) ? task.tags.join(',') : task.tags });
  }
  
  const data = JSON.stringify(operations);
  
  return new Promise((resolve, reject) => {
    const req = https.request({
      hostname: 'dev.azure.com',
      path: `/${org}/${project}/_apis/wit/workitems/${type}?api-version=7.1`,
      method: 'POST',
      headers: {
        'Authorization': `Basic ${Buffer.from(`:${token}`).toString('base64')}`,
        'Content-Type': 'application/json-patch+json',
        'Content-Length': Buffer.byteLength(data)
      }
    }, (res) => {
      let body = '';
      res.on('data', chunk => body += chunk);
      res.on('end', () => {
        if (res.statusCode >= 200 && res.statusCode < 300) {
          resolve(JSON.parse(body));
        } else {
          reject(new Error(`HTTP ${res.statusCode}: ${body}`));
        }
      });
    });
    
    req.on('error', reject);
    req.write(data);
    req.end();
  });
}

async function confirmCreation(tasks) {
  console.log('\nThe following work items will be created:\n');
  tasks.forEach((t, i) => console.log(`  ${i + 1}. ${t.title}`));
  console.log('\n');
  
  return new Promise((resolve) => {
    process.stdin.resume();
    process.stdout.write('Proceed? (y/N): ');
    process.stdin.on('data', (data) => {
      const answer = data.toString().trim().toLowerCase();
      process.stdin.pause();
      resolve(answer === 'y' || answer === 'yes');
    });
  });
}

async function main() {
  const { org, project, type, token, input, 'dry-run': dryRun } = args;
  
  if (!org || !project || !type || !token || !input) {
    console.error('Usage: node scripts/export-tasks.js --org ORG --project PROJECT --type TYPE --token TOKEN --input FILE');
    console.error('\nOptions:');
    console.error('  --org       Azure DevOps organization name');
    console.error('  --project   Project name or ID');
    console.error('  --type      Work item type (Task, User Story, Bug)');
    console.error('  --token     Personal Access Token');
    console.error('  --input     Input file (Markdown table or JSON)');
    console.error('  --dry-run   Show what would be created (optional)');
    process.exit(1);
  }
  
  if (!fs.existsSync(input)) {
    console.error(`Error: Input file not found: ${input}`);
    process.exit(1);
  }
  
  const tasks = await parseInput(input);
  console.log(`Found ${tasks.length} tasks to export\n`);
  
  if (dryRun) {
    console.log('DRY RUN - Tasks that would be created:\n');
    tasks.forEach((t, i) => {
      console.log(`  ${i + 1}. ${t.title}`);
      if (t.description) console.log(`     Description: ${t.description.substring(0, 80)}...`);
      if (t.hours) console.log(`     Hours: ${t.hours}`);
    });
    return;
  }
  
  const proceed = await confirmCreation(tasks);
  if (!proceed) {
    console.log('Cancelled.');
    process.exit(0);
  }
  
  const results = { created: [], failed: [], summary: { total: tasks.length, created: 0, failed: 0 } };
  
  for (let i = 0; i < tasks.length; i++) {
    const task = tasks[i];
    process.stdout.write(`Creating ${i + 1}/${tasks.length}: ${task.title}... `);
    
    try {
      const result = await createWorkItem(org, project, type, token, task);
      results.created.push({
        id: result.id,
        title: task.title,
        url: result.url
      });
      results.summary.created++;
      console.log(`ID: ${result.id}\n`);
    } catch (err) {
      results.failed.push({
        title: task.title,
        error: err.message
      });
      results.summary.failed++;
      console.log(`FAILED - ${err.message}\n`);
    }
  }
  
  console.log('\n========================================');
  console.log('              SUMMARY                  ');
  console.log('========================================');
  console.log(`Total Tasks: ${results.summary.total}`);
  console.log(`Created:     ${results.summary.created}`);
  console.log(`Failed:      ${results.summary.failed}`);
  console.log('========================================\n');
  
  fs.writeFileSync('azure-devops-results.json', JSON.stringify(results, null, 2));
  console.log('Results saved to azure-devops-results.json');
}

main().catch(console.error);
