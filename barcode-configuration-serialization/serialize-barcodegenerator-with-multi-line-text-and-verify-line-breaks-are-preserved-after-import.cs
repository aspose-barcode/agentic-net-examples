// Title: Serialize BarcodeGenerator with multi-line text and verify line breaks
// Description: Demonstrates how to serialize a BarcodeGenerator containing multi‑line text to XML and ensures that line‑break characters are retained after deserialization.
// Category-Description: This example belongs to the Aspose.BarCode serialization category, illustrating the use of BarcodeGenerator, ExportToXml, and ImportFromXml for persisting barcode settings. Developers often need to store barcode configurations, share them across services, or archive them, and must guarantee that text data, including line breaks, remains unchanged during the process. The snippet shows best practices for handling multi‑line CodeText and validating integrity after import.
// Prompt: Serialize a BarcodeGenerator with multi‑line text and verify line breaks are preserved after import.
// Tags: barcode symbology, serialization, xml, code128, barcodelibrary

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that serializes a <see cref="BarcodeGenerator"/> with multi‑line text to XML,
/// then imports it back to verify that line‑break characters are preserved.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs serialization, deserialization, and validation.
    /// </summary>
    static void Main()
    {
        // Define a multi‑line text containing different line‑break characters.
        string originalText = "Line1\r\nLine2\nLine3\rLine4";

        // Determine the full path for the temporary XML file used for serialization.
        string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.xml");

        // Create a BarcodeGenerator, assign the multi‑line text, and export its settings to XML.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = originalText;
            generator.ExportToXml(xmlPath);
        }

        // Ensure the XML file was successfully created before proceeding.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Failed to create the XML file.");
            return;
        }

        // Import the barcode generator from the previously saved XML file.
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Compare the imported CodeText with the original to verify line‑break preservation.
            bool isPreserved = importedGenerator.CodeText == originalText;
            Console.WriteLine($"Line breaks preserved after import: {isPreserved}");
        }

        // Attempt to delete the temporary XML file; ignore any errors during cleanup.
        try
        {
            File.Delete(xmlPath);
        }
        catch
        {
            // Cleanup failure is non‑critical; no action required.
        }
    }
}