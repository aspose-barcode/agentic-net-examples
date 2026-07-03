# Contributing

Thank you for contributing to the Aspose.BarCode for .NET agentic examples repository.

This repository is **mostly generated** by the Aspose.BarCode Examples Generator. The generator produces `.cs` examples, the root and per-category `agents.md` guides, `index.json`, `README.md`, and `CONTRIBUTING.md`. Manual contributions are welcome for one-off fixes and additions, but should follow the rules below so the next regeneration does not undo your changes.

## How to Add or Fix a Standalone `.cs` Example

1. **Locate the right category folder.** Categories are kebab-case, e.g. `barcode-generation/`. Pick the folder that best matches the API surface your example exercises.
2. **Create a single self-contained `.cs` file** in that folder. The filename must be kebab-case and end in `.cs`. The file must compile and run as a standalone .NET 9 console application.
3. **Match the existing code style.** Use the patterns demonstrated in neighbouring examples:
   - `using` blocks with curly braces for all `IDisposable` types (never `using var`)
   - `Aspose.Drawing` for all drawing types — never `System.Drawing` (unavailable on .NET 9)
   - Always use the `.Barcode.` sub-object for barcode parameters: `generator.Parameters.Barcode.XDimension`, not `generator.Parameters.XDimension`
   - `Console.WriteLine` for success output, `Console.Error.WriteLine` for errors
4. **Do not include a `.csproj`.** The validation workflow synthesises one using the package version from `index.json` — shipping a `.csproj` per example would conflict.
5. **Reference input files defensively.** If your example expects an image or barcode file, check existence with `File.Exists` and print a message on miss. The validator runs `dotnet run` with no input files; runtime failure on missing input is non-blocking.

## Generated-File Policy

The generator emits these files. **Do not hand-edit them** — the next regeneration overwrites manual changes:

- `agents.md` (root and every category folder)
- `index.json` (root)
- `README.md` (root)
- `CONTRIBUTING.md` (root)

If you find a problem in one of these, fix it upstream in the generator, then trigger a docs regeneration via `POST /github/docs/update`. The exception: you may add new sections to `CONTRIBUTING.md` — the generator only overwrites it when the content hash changes.

## Local Validation

Before opening a PR, validate that the example builds and runs against the pinned NuGet version:

```bash
# From any working directory
mkdir /tmp/barcode-validate && cd /tmp/barcode-validate
dotnet new console --framework net9.0
cp /path/to/your-example.cs Program.cs
dotnet add package Aspose.BarCode --version 26.6.0
dotnet build --nologo /p:WarningLevel=0
dotnet run --no-build || true   # runtime failure on missing input is OK
```

The build must complete with `Build succeeded` and zero errors. Runtime errors from missing input files (e.g. `sample.png`) are tolerated — the build step is the gate.

## Common Build Errors

| Error | Cause | Fix |
|-------|-------|-----|
| `CS0246: The type 'Bitmap' could not be found` | `using System.Drawing` included | Replace with `using Aspose.Drawing` |
| `CS1061: 'BarcodeParameters' does not contain 'XDimension'` | Missing `.Barcode.` sub-object | Use `generator.Parameters.Barcode.XDimension` |
| `CS0103: 'Color' does not exist` | `System.Drawing.Color` assumed | Use `Aspose.Drawing.Color` |
| `CS0029: Cannot implicitly convert 'int' to 'string'` | `MailmarkCodetext.Class` is a string | Assign as quoted string: `mailmark.Class = "1"` |
| `CS0246: 'ReadBarCodesAsync'` | Method does not exist | Use synchronous `reader.ReadBarCodes()` |

## Pull Request Notes

When opening a PR, describe:

- Which examples or categories changed.
- The exact validation commands you ran and the outcome.
- Whether you ran the binary (vs. only compiling).
- Any input files your example needs that are not in the repo.
- Confirmation that you did **not** hand-edit generated metadata (`agents.md`, `index.json`, `README.md`).

PRs that touch only generated files without a corresponding generator change will be asked to redirect upstream.
