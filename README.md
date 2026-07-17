# Aspose.BarCode for .NET — Agentic Examples

Agentic, build-validated C# code examples for **Aspose.BarCode for .NET** covering barcode generation, recognition, and configuration across 60+ symbologies. Every example compiles and runs successfully. Includes `agents.md` guides optimized for AI coding agents.

## About Aspose.BarCode for .NET

[Aspose.BarCode for .NET](https://products.aspose.com/barcode/net/) is a powerful barcode generation and recognition library for .NET applications. It enables developers to generate, read, and process barcodes programmatically without any third-party dependencies.

**Key capabilities:**
- Generate barcodes for 60+ symbologies including Code128, QR Code, DataMatrix, PDF417, Aztec, EAN, UPC, Code39, ITF14, and more
- Read and recognize barcodes from images, PDFs, and memory streams
- Control barcode appearance — size, resolution, colors, padding, rotation, and XDimension
- Configure checksum settings per symbology
- Export barcode images as PNG, JPEG, SVG, EMF, TIFF, and BMP
- Embed barcodes into PDF documents, Word files, and PowerPoint presentations
- Work with complex barcode types — Swiss QR Bill, Mailmark, HIBC LIC, GS1 Composite, MaxiCode
- Serialize and deserialize barcode generator and reader configurations to XML

## Install

```bash
dotnet add package Aspose.BarCode
```

Or via NuGet Package Manager:
```
Install-Package Aspose.BarCode
```

Requires .NET SDK 10.0 or later.

## Statistics

| Metric | Value |
|--------|-------|
| Total Examples | 1124 |
| Categories | 21 |
| Overall Pass Rate | 100.0% |
| Package Version | Aspose.BarCode 26.6.0 |
| Last Updated | 2026-07-17 |

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

## Categories

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
| [Barcode Saving and Export](./barcode-saving-and-export/) | 30 | 100.0% | [agents.md](./barcode-saving-and-export/agents.md) |
| [Barcode Size and Resolution](./barcode-size-and-resolution/) | 34 | 100.0% | [agents.md](./barcode-size-and-resolution/agents.md) |
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

## How to Use

Each example is a self-contained C# file. Clone the repository and run any example directly:

```bash
git clone https://github.com/aspose-barcode/agentic-net-examples.git
cd agentic-net-examples

# Navigate to a category
cd barcode-recognition-basics

# Create a console project, add the package, and run an example
dotnet new console -o ExampleRunner
cd ExampleRunner
dotnet add package Aspose.BarCode

# Copy any .cs file from the category as Program.cs and run
cp ../generate-code128-barcode-and-save-as-png.cs Program.cs
dotnet run
```

## Example

Generate a Code128 barcode and save it as a PNG file:

```csharp
using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 50f;
            generator.Save("code128.png");
            Console.WriteLine("Barcode saved as code128.png");
        }
    }
}
```

Read a barcode from an image file:

```csharp
using System;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        using (var reader = new BarCodeReader("barcode.png", DecodeType.AllSupportedTypes))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"Text: {result.CodeText}");
            }
        }
    }
}
```

## For AI Coding Agents

This repository includes an [`agents.md`](./agents.md) file at the root and in each category folder. These files provide AI coding agents with:

- Required namespaces and using directives for each category
- Correct API property paths and patterns
- Common pitfalls and prohibited patterns
- Example code fragments for key operations

If you are an AI coding agent generating Aspose.BarCode for .NET code, read the relevant `agents.md` before generating examples to avoid common API mistakes.

## Prerequisites

- .NET SDK 10.0 or later
- Aspose.BarCode for .NET 26.6.0 (via NuGet)

A valid [Aspose license](https://purchase.aspose.com/buy) is required for production use. For evaluation, examples run without a license but output may include watermarks on generated barcode images.

## Agentic .NET Ecosystem

Other Aspose products with agentic, build-validated example repositories:

| Product | Repository | Focus |
|---------|-----------|-------|
| Aspose.Words for .NET | [aspose-words/agentic-net-examples](https://github.com/aspose-words/agentic-net-examples) | Word processing, DOCX, mail merge |
| Aspose.Cells for .NET | [aspose-cells/agentic-net-examples](https://github.com/aspose-cells/agentic-net-examples) | Spreadsheets, Excel, charts |
| Aspose.HTML for .NET | [aspose-html/agentic-net-examples](https://github.com/aspose-html/agentic-net-examples) | HTML conversion, DOM editing |
| Aspose.Imaging for .NET | [aspose-imaging/agentic-net-examples](https://github.com/aspose-imaging/agentic-net-examples) | Image conversion, manipulation |
| Aspose.Slides for .NET | [aspose-slides/agentic-net-examples](https://github.com/aspose-slides/agentic-net-examples) | Presentations, PowerPoint |
| Aspose.Email for .NET | [aspose-email/agentic-net-examples](https://github.com/aspose-email/agentic-net-examples) | Email, calendars, messaging |
| Aspose.PDF for .NET | [aspose-pdf/agentic-net-examples](https://github.com/aspose-pdf/agentic-net-examples) | PDF creation, conversion and manipulation |

## Related Resources

### Official Documentation
- [Aspose.BarCode for .NET Documentation](https://docs.aspose.com/barcode/net/) — Guides, tutorials, and feature overviews
- [API Reference](https://reference.aspose.com/barcode/net/) — Complete class/method reference
- [Release Notes](https://releases.aspose.com/barcode/net/release-notes/) — Version history and changelogs

### Downloads & Packages
- [NuGet Package](https://www.nuget.org/packages/Aspose.BarCode/) — Install via `dotnet add package Aspose.BarCode`
- [Direct Downloads](https://releases.aspose.com/barcode/net/) — MSI/ZIP installers and DLLs

### Community & Support
- [Aspose.BarCode Forum](https://forum.aspose.com/c/barcode/13) — Community Q&A and official support
- [Aspose Blog - BarCode](https://blog.aspose.com/category/barcode/) — Tutorials, tips, and product updates
- [GitHub Issues](https://github.com/aspose-barcode/agentic-net-examples/issues) — Bug reports and feature requests

### Licensing & Purchase
- [Purchase](https://purchase.aspose.com/buy) — Commercial license options
- [Temporary License](https://purchase.aspose.com/temporary-license/) — Full-feature evaluation license

## License

All examples use [Aspose.BarCode for .NET](https://products.aspose.com/barcode/net/) and require a valid license for production use. See [licensing options](https://purchase.aspose.com/buy).

---

*This repository is maintained by automated code generation. For AI-friendly guidance, see [agents.md](./agents.md). Last updated: 2026-07-17*

<!-- AUTOGENERATED:START -->
Updated: 2026-07-17 | Examples: 1124 | Categories: 21 | Package: Aspose.BarCode 26.6.0
<!-- AUTOGENERATED:END -->
