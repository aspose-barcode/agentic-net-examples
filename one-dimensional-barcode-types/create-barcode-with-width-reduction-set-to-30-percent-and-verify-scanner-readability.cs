// Title: Create Code128 barcode with 30% width reduction and verify readability
// Description: Demonstrates how to generate a Code128 barcode with a 30 percent bar‑width reduction, save it as PNG, and confirm that a scanner can read it.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating the use of BarcodeGenerator for customizing barcode appearance (e.g., BarWidthReduction) and BarCodeReader for validating scan results. Developers often need to adjust visual parameters while ensuring scanner compatibility, making this pattern useful for packaging, inventory, and logistics applications.
// Prompt: Create a barcode with width reduction set to 30 percent and verify scanner readability.
// Tags: code128, width-reduction, barcode-generation, barcode-recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a 30 percent bar‑width reduction,
/// saves it as a PNG image, and verifies that the barcode can be read
/// by a scanner using Aspose.BarCode's recognition API.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode creation,
    /// saves the image, and validates readability.
    /// </summary>
    static void Main()
    {
        // Define output file and barcode content
        const string outputPath = "barcode.png";
        const string codeText = "1234567890";

        // --------------------------------------------------------------------
        // Generate the barcode with custom visual parameters
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply a 30 percent reduction to the bar width
            generator.Parameters.Barcode.BarWidthReduction.Point = 30f;

            // Set foreground (bars) and background colors for clear contrast
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the generated image exists before attempting recognition
        // --------------------------------------------------------------------
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to generate barcode image at '{outputPath}'.");
            return;
        }

        // --------------------------------------------------------------------
        // Use BarCodeReader to scan the saved image and confirm readability
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.AllSupportedTypes))
        {
            bool readable = false;

            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");

                // Check if the detected text matches the original input
                if (result.CodeText == codeText)
                {
                    readable = true;
                }
            }

            // Output final verification result
            Console.WriteLine(readable
                ? "Barcode is readable by the scanner."
                : "Barcode could not be read correctly.");
        }
    }
}