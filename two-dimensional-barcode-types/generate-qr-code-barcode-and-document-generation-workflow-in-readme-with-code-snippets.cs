// Title: Generate QR Code and Create README with Code Snippet
// Description: Demonstrates how to generate a QR Code barcode image using Aspose.BarCode and save it as a PNG file, then produce a README.md containing the sample code.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation. It showcases the use of BarcodeGenerator, EncodeTypes, and image format settings. Typical scenarios include embedding QR codes in documents, web pages, or marketing materials, where developers need to customize error correction levels and colors. The example also illustrates file I/O for documentation purposes, a common task when automating build pipelines or generating sample resources.
// Prompt: Generate QR Code barcode and document generation workflow in README with code snippets.
// Tags: qr code, barcode generation, png, readme, aspose.barcode, aspose.drawing, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an entry point that generates a QR Code image and creates a README file with the corresponding C# code snippet.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR Code barcode, saves it as a PNG image, and writes a README.md file containing the example code.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Define output directory and file paths
        // ------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir); // Ensure the output folder exists

        string qrImagePath = Path.Combine(outputDir, "qr.png");
        string readmePath = Path.Combine(outputDir, "README.md");

        // ------------------------------------------------------------
        // Generate QR Code barcode using Aspose.BarCode
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a high error correction level (Level H) for better resilience
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optional: define foreground (barcode) and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated QR Code as a PNG image
            generator.Save(qrImagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Create a README file that includes the sample code snippet
        // ------------------------------------------------------------
        string readmeContent = @"# QR Code Generation Example

This example demonstrates how to generate a QR Code barcode using Aspose.BarCode and save it as a PNG image.

```csharp
class Program
{
    static void Main()
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, ""https://example.com""))
        {
            // Set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Set colors (optional)
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the QR Code image
            generator.Save(""qr.png"");
        }
    }
}
```";

        File.WriteAllText(readmePath, readmeContent);

        // ------------------------------------------------------------
        // Output locations to the console for user awareness
        // ------------------------------------------------------------
        Console.WriteLine($"QR Code image saved to: {qrImagePath}");
        Console.WriteLine($"README generated at: {readmePath}");
    }
}