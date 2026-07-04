// Title: Serialize BarcodeGenerator with Multi-line Text
// Description: Demonstrates exporting a BarcodeGenerator containing multi-line CodeText to XML and verifying that line breaks are retained after importing.
// Prompt: Serialize a BarcodeGenerator with multi‑line text and verify line breaks are preserved after import.
// Tags: barcode, code128, serialization, xml, multiline, aspnet.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that shows how to serialize a <see cref="BarcodeGenerator"/> with multi‑line text,
/// export its settings to XML, and confirm that line breaks are preserved after re‑importing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the export/import cycle and validates line‑break preservation.
    /// </summary>
    static void Main()
    {
        // Define temporary file paths for the XML settings and the optional PNG image.
        string xmlPath = "barcode.xml";
        string imagePath = "barcode.png";

        // Original multi‑line text to be encoded in the barcode.
        string originalText = "Line1\r\nLine2\r\nLine3";

        // Create a BarcodeGenerator for Code128 and assign the multi‑line CodeText.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = originalText;

            // Save a visual representation of the barcode (optional, for verification).
            generator.Save(imagePath);

            // Export the generator's configuration, including the CodeText, to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // Ensure the XML file was created before attempting to import it.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Error: XML file was not created.");
            return;
        }

        // Import the generator settings from the XML file.
        using (BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Retrieve the CodeText from the imported generator.
            string importedText = importedGenerator.CodeText;

            // Verify that the line breaks in the imported text match the original.
            bool isPreserved = importedText == originalText;

            Console.WriteLine("Line breaks preserved: " + isPreserved);
            Console.WriteLine("Imported CodeText:");
            Console.WriteLine(importedText);
        }
    }
}