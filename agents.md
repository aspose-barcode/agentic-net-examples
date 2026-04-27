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
- Each `.cs` file is a **standalone Console Application** - do not create multi-file projects
- All examples must **compile and run** without errors using `dotnet build` and `dotnet run`
- Follow the conventions, boundaries, and anti-patterns documented below exactly
- Use the **Command Reference** section for build/run commands

## Repository Overview

This repository contains **1124** working code examples demonstrating Aspose.BarCode for .NET capabilities.

**Statistics** (as of 2026-04-27):
- Total Examples: 1124
- Categories: 21
- Overall Pass Rate: 100.0%

## Category Details

### barcode-appearance-customization
- Examples: 30
- Guide: [agents.md](./barcode-appearance-customization/agents.md)

### barcode-checksum-control
- Examples: 30
- Guide: [agents.md](./barcode-checksum-control/agents.md)

### barcode-color-customization
- Examples: 28
- Guide: [agents.md](./barcode-color-customization/agents.md)

### barcode-configuration-serialization
- Examples: 30
- Guide: [agents.md](./barcode-configuration-serialization/agents.md)

### barcode-reading-properties
- Examples: 50
- Guide: [agents.md](./barcode-reading-properties/agents.md)

### barcode-recognition-basics
- Examples: 75
- Guide: [agents.md](./barcode-recognition-basics/agents.md)

### barcode-recognition-performance
- Examples: 81
- Guide: [agents.md](./barcode-recognition-performance/agents.md)

### barcode-recognition-xml-serialization
- Examples: 35
- Guide: [agents.md](./barcode-recognition-xml-serialization/agents.md)

### barcode-saving-and-export
- Examples: 30
- Guide: [agents.md](./barcode-saving-and-export/agents.md)

### barcode-size-and-resolution
- Examples: 34
- Guide: [agents.md](./barcode-size-and-resolution/agents.md)

### barcode-text-customization
- Examples: 29
- Guide: [agents.md](./barcode-text-customization/agents.md)

### gs1-barcode-types
- Examples: 34
- Guide: [agents.md](./gs1-barcode-types/agents.md)

### hibc-lic-barcode
- Examples: 35
- Guide: [agents.md](./hibc-lic-barcode/agents.md)

### mailmark-four-state-barcode
- Examples: 29
- Guide: [agents.md](./mailmark-four-state-barcode/agents.md)

### mailmark-two-dimensional-barcode
- Examples: 34
- Guide: [agents.md](./mailmark-two-dimensional-barcode/agents.md)

### maxicode-barcode
- Examples: 34
- Guide: [agents.md](./maxicode-barcode/agents.md)

### one-dimensional-barcode-types
- Examples: 126
- Guide: [agents.md](./one-dimensional-barcode-types/agents.md)

### postal-barcode-types
- Examples: 90
- Guide: [agents.md](./postal-barcode-types/agents.md)

### special-barcode-recognition-settings
- Examples: 48
- Guide: [agents.md](./special-barcode-recognition-settings/agents.md)

### swiss-qr-code
- Examples: 28
- Guide: [agents.md](./swiss-qr-code/agents.md)

### two-dimensional-barcode-types
- Examples: 214
- Guide: [agents.md](./two-dimensional-barcode-types/agents.md)

## Boundaries

### Always

These rules are mandatory for every example.

#### Use `using` blocks for IDisposable objects
```csharp
// CORRECT
using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
{
    generator.Save("barcode.png");
}

// WRONG - memory leak, file lock not released
// BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345");
// generator.Save("barcode.png");
```

#### Use Aspose.Drawing instead of System.Drawing
```csharp
// CORRECT
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

// WRONG - System.Drawing is not available in this environment
// using System.Drawing;
```

#### Never use EncodeTypes as a variable or parameter type
```csharp
// CORRECT
BaseEncodeType barcodeType = EncodeTypes.Code128;
void Generate(BaseEncodeType type, string codeText) { }

// WRONG - EncodeTypes is a static class, not a type
// EncodeTypes barcodeType = EncodeTypes.Code128;
// void Generate(EncodeTypes type, string codeText) { }
```

#### Always use the Barcode sub-object for barcode-specific parameters
```csharp
// CORRECT
generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
generator.Parameters.Barcode.XDimension.Pixels = 2f;
generator.Parameters.Barcode.BarHeight.Pixels = 50f;
generator.Parameters.Barcode.Padding.Left.Point = 5f;

// WRONG
// generator.Parameters.BarColor = Color.Black;
// generator.Parameters.XDimension.Pixels = 2f;
```

#### Use unit members for unit-based properties
```csharp
// CORRECT
generator.Parameters.Barcode.XDimension.Pixels = 2f;
generator.Parameters.Barcode.BarHeight.Point = 50f;
generator.Parameters.ImageWidth.Millimeters = 50f;

// WRONG
// generator.Parameters.Barcode.XDimension = 2f;
// generator.Parameters.Barcode.BarHeight = Unit.Pixels(50);
```

#### Use correct path for border properties
```csharp
// CORRECT
generator.Parameters.Border.Color = Aspose.Drawing.Color.Black;
generator.Parameters.Border.Width.Pixels = 2f;

// WRONG
// generator.Parameters.BorderColor = Color.Black;
// generator.Parameters.BorderWidth = 2;
```

### Ask First

Check with a human before doing any of these:
- **Creating multi-file projects** - each example must be a single `.cs` file
- **Using deprecated APIs** - check the Aspose.BarCode changelog for the current API surface
- **Adding NuGet packages** beyond `Aspose.BarCode` - the `.csproj` template only includes Aspose.BarCode
- **Modifying shared infrastructure** - `.csproj` templates, `agents.md` files

### Never

- Never use `System.Drawing` — always use `Aspose.Drawing`
- Never use `EncodeTypes` as a variable, field, or parameter type — use `BaseEncodeType`
- Never call `Environment.Exit(1)` — use `Console.WriteLine` for failure reporting
- Never use `generator.Parameters.BorderColor` or `generator.Parameters.BorderWidth` — use `generator.Parameters.Border.Color` and `generator.Parameters.Border.Width.Pixels`
- Never use `generator.Parameters.Padding.*` — use `generator.Parameters.Barcode.Padding.*`
- Never use `generator.Parameters.XDimension` — use `generator.Parameters.Barcode.XDimension`
- Never use `reader.BarcodeSettings.QualitySettings` — use `reader.QualitySettings` directly
- Never use `result.IsCodeTextValid` — it does not exist on `BarCodeResult`
- Never use `DeconvolutionMode.Disabled` — use `DeconvolutionMode.Fast`
- Never forget to dispose objects — always use `using` blocks for IDisposable types
- Never use `using (var doc = new Document())` or `using (var builder = new DocumentBuilder(doc))` — these types are not IDisposable

## Common Mistakes (Anti-Patterns)

### Using System.Drawing instead of Aspose.Drawing
```csharp
// WRONG - CS0246: System.Drawing not available
using System.Drawing;
Color c = Color.Red;
```
```csharp
// CORRECT
using Aspose.Drawing;
Aspose.Drawing.Color c = Aspose.Drawing.Color.Red;
```

### Wrong border property paths
```csharp
// WRONG - CS1061: member does not exist
generator.Parameters.BorderColor = Color.Black;
generator.Parameters.BorderWidth = 2;
```
```csharp
// CORRECT
generator.Parameters.Border.Color = Aspose.Drawing.Color.Black;
generator.Parameters.Border.Width.Pixels = 2f;
```

### Wrong padding / XDimension paths
```csharp
// WRONG
generator.Parameters.Padding.Left.Pixels = 5f;
generator.Parameters.XDimension.Pixels = 2f;
```
```csharp
// CORRECT
generator.Parameters.Barcode.Padding.Left.Pixels = 5f;
generator.Parameters.Barcode.XDimension.Pixels = 2f;
```

### Using EncodeTypes as a type
```csharp
// WRONG - CS0721: static type cannot be used as parameter type
void Generate(EncodeTypes type) { }
```
```csharp
// CORRECT
void Generate(BaseEncodeType type) { }
```

## Repository Structure

```
agents.md
README.md
+-- barcode-appearance-customization/
+-- barcode-checksum-control/
+-- barcode-color-customization/
+-- barcode-configuration-serialization/
+-- barcode-reading-properties/
+-- barcode-recognition-basics/
+-- barcode-recognition-performance/
+-- barcode-recognition-xml-serialization/
+-- barcode-saving-and-export/
+-- barcode-size-and-resolution/
+-- barcode-text-customization/
+-- gs1-barcode-types/
+-- hibc-lic-barcode/
+-- mailmark-four-state-barcode/
+-- mailmark-two-dimensional-barcode/
+-- maxicode-barcode/
+-- one-dimensional-barcode-types/
+-- postal-barcode-types/
+-- special-barcode-recognition-settings/
+-- swiss-qr-code/
+-- two-dimensional-barcode-types/
```

## Category Index

| Category | Examples | Pass Rate | Details |
|----------|----------|-----------|---------|
| [Barcode Appearance Customization](./barcode-appearance-customization/) | 30 | 100.0% | [agents.md](./barcode-appearance-customization/agents.md) |
| [Barcode Checksum Control](./barcode-checksum-control/) | 30 | 100.0% | [agents.md](./barcode-checksum-control/agents.md) |
| [Barcode Color Customization](./barcode-color-customization/) | 28 | 100.0% | [agents.md](./barcode-color-customization/agents.md) |
| [Barcode Configuration Serialization](./barcode-configuration-serialization/) | 30 | 100.0% | [agents.md](./barcode-configuration-serialization/agents.md) |
| [Barcode Reading Properties](./barcode-reading-properties/) | 50 | 100.0% | [agents.md](./barcode-reading-properties/agents.md) |
| [Barcode Recognition Basics](./barcode-recognition-basics/) | 75 | 100.0% | [agents.md](./barcode-recognition-basics/agents.md) |
| [Barcode Recognition Performance](./barcode-recognition-performance/) | 81 | 100.0% | [agents.md](./barcode-recognition-performance/agents.md) |
| [Barcode Recognition XML Serialization](./barcode-recognition-xml-serialization/) | 35 | 100.0% | [agents.md](./barcode-recognition-xml-serialization/agents.md) |
| [Barcode Saving And Export](./barcode-saving-and-export/) | 30 | 100.0% | [agents.md](./barcode-saving-and-export/agents.md) |
| [Barcode Size And Resolution](./barcode-size-and-resolution/) | 34 | 100.0% | [agents.md](./barcode-size-and-resolution/agents.md) |
| [Barcode Text Customization](./barcode-text-customization/) | 29 | 100.0% | [agents.md](./barcode-text-customization/agents.md) |
| [GS1 Barcode Types](./gs1-barcode-types/) | 34 | 100.0% | [agents.md](./gs1-barcode-types/agents.md) |
| [HIBC LIC Barcode](./hibc-lic-barcode/) | 35 | 100.0% | [agents.md](./hibc-lic-barcode/agents.md) |
| [Mailmark Four State Barcode](./mailmark-four-state-barcode/) | 29 | 100.0% | [agents.md](./mailmark-four-state-barcode/agents.md) |
| [Mailmark Two Dimensional Barcode](./mailmark-two-dimensional-barcode/) | 34 | 100.0% | [agents.md](./mailmark-two-dimensional-barcode/agents.md) |
| [MaxiCode Barcode](./maxicode-barcode/) | 34 | 100.0% | [agents.md](./maxicode-barcode/agents.md) |
| [One Dimensional Barcode Types](./one-dimensional-barcode-types/) | 126 | 100.0% | [agents.md](./one-dimensional-barcode-types/agents.md) |
| [Postal Barcode Types](./postal-barcode-types/) | 90 | 100.0% | [agents.md](./postal-barcode-types/agents.md) |
| [Special Barcode Recognition Settings](./special-barcode-recognition-settings/) | 48 | 100.0% | [agents.md](./special-barcode-recognition-settings/agents.md) |
| [Swiss QR Code](./swiss-qr-code/) | 28 | 100.0% | [agents.md](./swiss-qr-code/agents.md) |
| [Two Dimensional Barcode Types](./two-dimensional-barcode-types/) | 214 | 100.0% | [agents.md](./two-dimensional-barcode-types/agents.md) |

## Command Reference

### Build and Run
```bash
# Create a new project (if needed)
dotnet new console -n ExampleProject --framework net9.0

# Add Aspose.BarCode NuGet package
dotnet add package Aspose.BarCode

# Build
dotnet build --configuration Release --verbosity minimal

# Run
dotnet run
```

### Project File (.csproj)
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Aspose.BarCode" Version="26.4.0" />
  </ItemGroup>
</Project>
```

## Testing Guide

Every example must pass these verification steps.

### Build Verification
```bash
dotnet build --configuration Release --verbosity minimal
```
- **Success**: Exit code 0, no `CS` error codes in output
- **Failure**: Any `error CS####` line indicates a build failure

### Common Error Codes
| Code | Meaning | Fix |
|------|---------|-----|
| `CS0721` | Static type used as parameter | Use `BaseEncodeType` instead of `EncodeTypes` |
| `CS1061` | Member does not exist on type | Check Aspose.BarCode API docs; verify property path |
| `CS0246` | Type or namespace not found | Add missing `using` directive; use `Aspose.Drawing` not `System.Drawing` |
| `CS1674` | Type not IDisposable | Remove `using` from non-disposable types |
| `CS0029` | Cannot convert type | Cast explicitly or use correct type |

## How to Use These Examples

### Prerequisites
- .NET SDK (net9.0)
- Aspose.BarCode for .NET 26.4 (via NuGet)

### Running an Example
1. Navigate to any category folder
2. Each .cs file is a standalone Console Application
3. Compile and run:
   ```bash
   dotnet run <example-file.cs>
   ```

<!-- AUTOGENERATED:START -->
Updated: 2026-04-27 | Run: `20260427` | Examples: 1124 | Categories: 21
<!-- AUTOGENERATED:END -->

---
*This repository is maintained by automated code generation. Last updated: 2026-04-27 | Total examples: 1124*
