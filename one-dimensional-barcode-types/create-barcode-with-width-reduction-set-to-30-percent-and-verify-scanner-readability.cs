// Title: Generate Code128 Barcode with 30% Width Reduction and Verify Readability
// Description: This example creates a Code128 barcode, applies a 30 percent bar‑width reduction, saves it as a PNG image, and then reads it back to confirm scanner readability.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows. It showcases the use of BarcodeGenerator to customize barcode appearance (e.g., bar‑width reduction) and BarCodeReader to validate that the produced image can be decoded. Typical for developers who need to fine‑tune barcode dimensions for space‑constrained layouts and ensure downstream scanning reliability. Ideal for collections of examples on barcode customization, image output, and verification using Aspose.BarCode for .NET.
/// Prompt: Create a barcode with width reduction set to 30 percent and verify scanner readability.
/// Tags: code128, width reduction, barcode generation, barcode recognition, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a Code128 barcode with a 30 percent bar‑width reduction,
/// saving it as PNG, and verifying that it can be read by a scanner.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and validates readability.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Create a barcode generator for Code128 with the sample text "1234567890".
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply a 30 percent bar‑width reduction (value expressed in points).
            generator.Parameters.Barcode.BarWidthReduction.Point = 30f;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to create barcode image at '{outputPath}'.");
            return;
        }

        // Use BarCodeReader to decode the saved image and confirm scanner readability.
        using (BarCodeReader reader = new BarCodeReader(outputPath, DecodeType.Code128))
        {
            bool found = false;

            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                found = true;
            }

            // Inform the user if no readable barcode was found.
            if (!found)
            {
                Console.WriteLine("No barcode detected or unreadable.");
            }
        }
    }
}