// Title: Validate barcode symbology from imported XML state
// Description: Demonstrates how to import a barcode generator from an XML file, verify that its symbology matches the expected type, and then generate an image if validation succeeds.
// Prompt: Write code to validate that an imported XML state contains the expected barcode symbology before processing results.
// Tags: barcode symbology, validation, xml import, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that validates the barcode symbology defined in an imported XML state before generating the barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Performs validation of the barcode symbology and generates an image if the validation passes.
    /// </summary>
    static void Main()
    {
        // Expected symbology name (e.g., "Code128")
        const string expectedSymbology = "Code128";

        // Path to the XML file that contains the barcode state
        const string xmlPath = "barcode_state.xml";

        // Verify that the XML file exists before attempting import
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        // Import the barcode generator from the XML state
        using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Retrieve the actual symbology type name from the imported generator
            string actualSymbology = generator.BarcodeType.TypeName;

            // Compare with the expected symbology (case‑insensitive)
            if (!string.Equals(actualSymbology, expectedSymbology, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Warning: Expected symbology '{expectedSymbology}' but found '{actualSymbology}'. Processing aborted.");
                return;
            }

            // Symbology matches – proceed with barcode processing (e.g., generate and save an image)
            const string outputImage = "validated_barcode.png";
            generator.Save(outputImage);
            Console.WriteLine($"Barcode symbology validated as '{actualSymbology}'. Image saved to '{outputImage}'.");
        }
    }
}