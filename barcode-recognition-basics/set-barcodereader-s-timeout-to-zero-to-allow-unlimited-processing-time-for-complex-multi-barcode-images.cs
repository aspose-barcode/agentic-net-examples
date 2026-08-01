// Title: Unlimited Timeout for BarCodeReader with Complex Images
// Description: Demonstrates setting BarCodeReader.Timeout to zero for unlimited processing time when reading multiple barcodes from an image.
// Category-Description: This example belongs to the Aspose.BarCode reading operations collection. It showcases how to use BarCodeReader and BarcodeGenerator to create a barcode image in memory and then decode all barcodes without a time limit. Developers working with high‑density or multi‑barcode images often need to adjust the timeout to avoid premature termination. The key API classes illustrated are BarcodeGenerator, BarCodeReader, and related encoding/recognition types.
// Prompt: Set BarCodeReader's TimeOut to zero to allow unlimited processing time for complex multi‑barcode images.
// Tags: code128, barcode reading, console output, barcodegenerator, barcodereader

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a barcode image in memory and reads all barcodes from it using an unlimited timeout.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a Code128 barcode, sets BarCodeReader.Timeout to zero,
    /// and prints detected barcode types and texts to the console.
    /// </summary>
    static void Main()
    {
        // Create a BarcodeGenerator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Generate the barcode image in memory
            using (var barcodeImage = generator.GenerateBarCodeImage())
            {
                // Initialize BarCodeReader without an image (will be set later)
                using (var reader = new BarCodeReader())
                {
                    // Set unlimited timeout (0 milliseconds) to handle complex images
                    reader.Timeout = 0;

                    // Assign the generated image to the reader for processing
                    reader.SetBarCodeImage(barcodeImage);

                    // Iterate through all detected barcodes and output their details
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}