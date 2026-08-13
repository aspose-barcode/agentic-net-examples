// Title: Verify visual property persistence after XML deserialization
// Description: Demonstrates creating a barcode, customizing visual properties, exporting to XML, importing back, and confirming that size, color, and text settings remain unchanged.
// Category-Description: This example belongs to the Aspose.BarCode serialization category, illustrating how to use BarcodeGenerator, ExportToXml, and ImportFromXml for persisting barcode configuration. Developers often need to store barcode settings in XML for later reuse, configuration files, or cross‑application sharing. The snippet shows typical use cases such as saving visual appearance, dimensions, and text attributes.
// Prompt: Verify that all visual properties such as size, color, and text persist after XML deserialization.
// Tags: barcode symbology, serialization, xml, visual properties, aspose.barcode, code128, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a barcode, customizes its visual appearance,
/// serializes the settings to XML, deserializes them, and verifies that all
/// visual properties persist correctly.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation,
    /// XML export/import, and property verification steps.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator with Code128 symbology and sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Configure visual properties: colors, dimensions, and text formatting.
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.Yellow;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Export the current generator settings to an in‑memory XML stream.
            using (var xmlStream = new MemoryStream())
            {
                generator.ExportToXml(xmlStream);
                xmlStream.Position = 0; // Reset stream position for reading.

                // Import a new generator instance from the XML data.
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlStream))
                {
                    // Compare each visual property between the original and imported generators.
                    bool barColorMatch = importedGenerator.Parameters.Barcode.BarColor.ToArgb() == generator.Parameters.Barcode.BarColor.ToArgb();
                    bool backColorMatch = importedGenerator.Parameters.BackColor.ToArgb() == generator.Parameters.BackColor.ToArgb();
                    bool widthMatch = Math.Abs(importedGenerator.Parameters.ImageWidth.Point - generator.Parameters.ImageWidth.Point) < 0.001f;
                    bool heightMatch = Math.Abs(importedGenerator.Parameters.ImageHeight.Point - generator.Parameters.ImageHeight.Point) < 0.001f;
                    bool fontFamilyMatch = importedGenerator.Parameters.Barcode.CodeTextParameters.Font.FamilyName == generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName;
                    bool fontSizeMatch = Math.Abs(importedGenerator.Parameters.Barcode.CodeTextParameters.Font.Size.Point - generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point) < 0.001f;
                    bool alignmentMatch = importedGenerator.Parameters.Barcode.CodeTextParameters.Alignment == generator.Parameters.Barcode.CodeTextParameters.Alignment;
                    bool codeTextMatch = importedGenerator.CodeText == generator.CodeText;

                    // Output verification results to the console.
                    Console.WriteLine($"BarColor persisted: {barColorMatch}");
                    Console.WriteLine($"BackColor persisted: {backColorMatch}");
                    Console.WriteLine($"ImageWidth persisted: {widthMatch}");
                    Console.WriteLine($"ImageHeight persisted: {heightMatch}");
                    Console.WriteLine($"FontFamily persisted: {fontFamilyMatch}");
                    Console.WriteLine($"FontSize persisted: {fontSizeMatch}");
                    Console.WriteLine($"TextAlignment persisted: {alignmentMatch}");
                    Console.WriteLine($"CodeText persisted: {codeTextMatch}");
                }
            }
        }
    }
}