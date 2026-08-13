// Title: Generate Code128 Barcode with 25% Width Reduction and Verify via Scanner Simulation
// Description: This example creates a Code128 barcode, reduces its bar width by 25 percent, saves it as a PNG, and then reads it back to simulate a handheld scanner verification.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows, focusing on barcode appearance customization (BarWidthReduction) and post‑generation validation. Uses BarcodeGenerator, BarCodeReader, and related parameter classes, typical for developers needing to fine‑tune barcode dimensions and ensure readability in real‑world scanning scenarios.
// Prompt: Create a barcode with width reduction of 25 percent and test readability with a handheld scanner.
// Tags: code128, width reduction, barcode generation, barcode recognition, png, aspose.barcode, handheld scanner simulation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a Code128 barcode with a 25 percent width reduction,
/// save it as an image, and verify its readability using Aspose.BarCode's recognition API.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and then reads it back
    /// to simulate scanning with a handheld device.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string barcodePath = "barcode.png";

        // ------------------------------------------------------------
        // Generate a Code128 barcode with a 25% bar width reduction.
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // BarWidthReduction is expressed in points; here we set it to 25 (interpreted as 25%).
            generator.Parameters.Barcode.BarWidthReduction.Point = 25f;

            // Save the generated barcode as a PNG image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{barcodePath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode image to simulate a handheld scanner scan.
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            bool found = false;

            // Iterate through all detected barcodes (there should be only one in this case).
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                found = true;
            }

            // Inform the user if no barcode was detected.
            if (!found)
            {
                Console.WriteLine("No barcode detected. Scanning may have failed.");
            }
        }
    }
}