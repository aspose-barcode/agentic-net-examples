// Title: Serialize QR BarcodeGenerator with Multi‑Line Text and Verify Line Break Preservation
// Description: Demonstrates how to generate a QR barcode containing multi‑line text, export its configuration to XML, re‑import it, and confirm that line breaks are retained.
// Category-Description: This example belongs to the Aspose.BarCode serialization category, illustrating the use of BarcodeGenerator, ExportToXml, and ImportFromXml for persisting barcode settings. Developers often need to save barcode configurations to files for later reuse or sharing across applications, especially when handling complex text such as multi‑line content. The snippet shows typical steps: create a generator, save image, export to XML, import back, and validate the CodeText.
// Prompt: Serialize a BarcodeGenerator with multi‑line text and verify line breaks are preserved after import.
// Tags: qr barcode, serialization, multiline text, exporttoxml, importfromxml, aspnet, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates serialization of a QR barcode with multi‑line text and verification of line‑break preservation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR barcode with multi‑line text, exports/imports its configuration,
    /// and checks that line breaks are preserved after deserialization.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeMultiLine_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the barcode image and the XML configuration
        string imagePath = Path.Combine(tempFolder, "barcode.png");
        string xmlPath = Path.Combine(tempFolder, "barcode.xml");

        // Multi‑line text (use LF for line breaks)
        string multiLineText = "First line\nSecond line\nThird line";

        // Create a QR barcode generator with the multi‑line text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, multiLineText))
        {
            // Save the barcode image (optional, just to show it works)
            generator.Save(imagePath, BarCodeImageFormat.Png);

            // Export the generator configuration to an XML file
            generator.ExportToXml(xmlPath);
        }

        // Import the generator from the saved XML configuration
        using (BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Verify that the CodeText (including line breaks) is preserved after import
            bool isPreserved = importedGenerator.CodeText == multiLineText;

            Console.WriteLine("Original CodeText:");
            Console.WriteLine(multiLineText);
            Console.WriteLine();

            Console.WriteLine("Imported CodeText:");
            Console.WriteLine(importedGenerator.CodeText);
            Console.WriteLine();

            Console.WriteLine("Line breaks preserved: " + (isPreserved ? "YES" : "NO"));
        }

        // Clean up temporary files (optional)
        try
        {
            if (File.Exists(imagePath)) File.Delete(imagePath);
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}