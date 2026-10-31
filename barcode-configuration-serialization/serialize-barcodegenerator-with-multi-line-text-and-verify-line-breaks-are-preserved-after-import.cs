// Title: Serialize BarcodeGenerator with Multi‑Line Text and Verify Line Break Preservation
// Description: Demonstrates how to serialize a BarcodeGenerator containing multi‑line text to XML, then import it back and confirm that line breaks are retained.
// Category-Description: This example belongs to the Aspose.BarCode serialization and deserialization category. It shows usage of BarcodeGenerator, its ExportToXml and ImportFromXml methods, and how to handle multi‑line CodeText. Developers often need to persist barcode settings, share them across services, or store them for later regeneration, making serialization essential.
// Prompt: Serialize a BarcodeGenerator with multi‑line text and verify line breaks are preserved after import.
// Tags: barcode, pdf417, serialization, deserialization, multiline, codetext, xml, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates serialization of a BarcodeGenerator with multi‑line text and verification of line‑break preservation after import.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a PDF417 barcode with multi‑line text, exports it to XML, re‑imports it, and checks line‑break integrity.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary working folder
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeMultiLine_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for XML and PNG images
        string xmlPath = Path.Combine(tempFolder, "generator.xml");
        string originalImagePath = Path.Combine(tempFolder, "barcode_original.png");
        string importedImagePath = Path.Combine(tempFolder, "barcode_imported.png");

        // Multi‑line code text (uses CRLF line breaks)
        string multiLineText = "Line1\r\nLine2";

        // Create a BarcodeGenerator, configure parameters, export to XML, and save the original image
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, multiLineText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2; // Set module size
            generator.ExportToXml(xmlPath);                     // Serialize settings to XML
            generator.Save(originalImagePath, BarCodeImageFormat.Png); // Save barcode image
        }

        // Import the generator from the previously saved XML and verify line‑break preservation
        using (var imported = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            string importedText = imported.CodeText;
            bool lineBreaksPreserved = importedText == multiLineText;
            Console.WriteLine("Line breaks preserved after import: " + lineBreaksPreserved);
            imported.Save(importedImagePath, BarCodeImageFormat.Png); // Save imported barcode image
        }

        // Optional cleanup: delete the temporary folder and its contents
        // Directory.Delete(tempFolder, true);
    }
}