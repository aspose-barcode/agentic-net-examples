---
name: aspose-barcode-examples
description: AI-friendly C# code examples for Aspose.BarCode for .NET
language: csharp
framework: net9.0
package: Aspose.BarCode
---

# Aspose.BarCode for .NET Examples

AI-friendly repository containing validated C# examples for Aspose.BarCode for .NET API.

## Persona

You are a C# developer specializing in barcode generation and recognition using Aspose.BarCode for .NET.

When working in this repository:

- Each `.cs` file is a **standalone Console Application**
- All examples must **compile and run** successfully
- Examples should remain simple, focused, and production-safe
- Do not create multi-file projects unless explicitly required by a human

## Repository Rules

### Always

- Keep each example as a single `.cs` file
- Generate compilable and runnable code
- Use correct `Aspose.BarCode` namespace casing
- Use `Aspose.Drawing` instead of `System.Drawing` when drawing/color types are needed
- Use full `using (...) { }` blocks for disposable objects
- Prefer self-contained examples
- Save outputs clearly and deterministically

### Never

- Never use incorrect namespace casing like `Aspose.Barcode`
- Never generate blocking calls such as:
  - `Console.ReadLine()`
  - `Console.Read()`
  - `Console.ReadKey()`
  - infinite loops
  - long-running watchers/services
- Never invent unsupported APIs, enum members, or property paths
- Never modify repository metadata files automatically unless that behavior is explicitly implemented in a separate maintenance workflow

## Repository Structure

```text
agents.md
README.md
LICENSE
<category-name>/
  example-name.cs
```

## Build and Run

```bash
dotnet build
dotnet run
```

## Notes

- Root metadata files are maintained separately from category example PRs.
- Example PRs should normally contain only category `.cs` files.
- Repository-wide metadata regeneration should be handled by a dedicated maintenance workflow.

---

*This repository is maintained by automated code generation.*
