// Title: Validate barcode symbology from imported XML state
// Description: Demonstrates how to load a barcode generator state from XML and verify that it uses the expected symbology before further processing.
// Category-Description: This example belongs to the Aspose.BarCode generation and validation category. It shows how to use BarcodeGenerator.ImportFromXml, access the BarcodeType property, and perform symbology checks. Typical use cases include validating saved barcode configurations, ensuring compatibility before rendering, and preventing processing of unexpected barcode types. Developers often need to read saved states, compare symbology, and conditionally generate images.
// Prompt: Write code to validate that an imported XML state contains the expected barcode symbology before processing results.
// Tags: barcode, symbology, validation, import, xml, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that validates the barcode symbology stored in an imported XML state
/// before generating the barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Loads a barcode generator from an XML file, checks its symbology,
    /// and generates an image only if the symbology matches the expected value.
    /// </summary>
    static void Main()
    {
        // Path to the XML file that contains the barcode generator state.
        string xmlPath = "barcode_state.xml";

        // Expected symbology name (e.g., "Code128", "QR", "DataMatrix").
        string expectedSymbology = "Code128";

        // Verify that the XML file exists before attempting import.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        try
        {
            // Import the barcode generator state from the XML file.
            using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // Retrieve the actual symbology of the imported generator.
                string actualSymbology = generator.BarcodeType.TypeName;

                // Compare the actual symbology with the expected value (case‑insensitive).
                if (string.Equals(actualSymbology, expectedSymbology, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Symbology validation succeeded: '{actualSymbology}'.");

                    // Proceed with further processing, e.g., generate and save the barcode image.
                    string outputImage = "generated_barcode.png";
                    generator.Save(outputImage);
                    Console.WriteLine($"Barcode image saved to '{outputImage}'.");
                }
                else
                {
                    // Symbology does not match; skip further processing.
                    Console.WriteLine($"Warning: Expected symbology '{expectedSymbology}' but found '{actualSymbology}'. Skipping processing.");
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during import or processing.
            Console.WriteLine($"Exception occurred: {ex.Message}");
        }
    }
}