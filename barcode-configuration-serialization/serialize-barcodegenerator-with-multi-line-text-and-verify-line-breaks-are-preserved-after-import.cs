// Title: Serialize BarcodeGenerator with Multi‑Line Text and Verify Line Break Preservation
// Description: Demonstrates how to generate a PDF417 barcode containing multi‑line text, export the generator configuration to XML, import it back, and confirm that line breaks are retained.
// Category-Description: This example belongs to the Aspose.BarCode serialization and configuration category. It shows how to use BarcodeGenerator, its Parameters, ExportToXml, and ImportFromXml methods to persist barcode settings. Typical use cases include saving barcode configurations for later reuse, sharing across services, or version‑controlling barcode definitions. Developers often need to serialize generators, modify XML, and ensure text formatting such as line breaks remains intact.
// Prompt: Serialize a BarcodeGenerator with multi‑line text and verify line breaks are preserved after import.
// Tags: pdf417, barcode, serialization, xml, multiline, codetext, aspnet, aspose.barcode, export, import

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates serialization of a <see cref="BarcodeGenerator"/> with multi‑line text and verification of line‑break preservation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, exports to XML, re‑imports, and validates code text integrity.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for output files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the XML configuration and PNG images
        string xmlPath = Path.Combine(tempDir, "generator.xml");
        string originalImagePath = Path.Combine(tempDir, "original.png");
        string loadedImagePath = Path.Combine(tempDir, "loaded.png");

        // Multi‑line barcode text (line breaks are represented by '\n')
        string multiLineText = "First line\nSecond line\nThird line";

        // Generate barcode and export its state to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, multiLineText))
        {
            // Ensure line breaks are allowed (default is false, which allows wrapping)
            generator.Parameters.Barcode.CodeTextParameters.NoWrap = false;

            // Save the original barcode image to PNG
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the generator configuration to an XML file
            generator.ExportToXml(xmlPath);
        }

        // Import barcode generator from the previously saved XML configuration
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);

        // Save the barcode image generated from the imported configuration
        importedGenerator.Save(loadedImagePath, BarCodeImageFormat.Png);

        // Verify that the code text (including line breaks) is preserved after import
        bool isPreserved = string.Equals(
            importedGenerator.CodeText,
            multiLineText,
            StringComparison.Ordinal);

        // Output verification results and file locations
        Console.WriteLine("Original CodeText:");
        Console.WriteLine(multiLineText);
        Console.WriteLine();
        Console.WriteLine("Imported CodeText:");
        Console.WriteLine(importedGenerator.CodeText);
        Console.WriteLine();
        Console.WriteLine("Line breaks preserved: " + isPreserved);
        Console.WriteLine("Files written to: " + tempDir);
    }
}