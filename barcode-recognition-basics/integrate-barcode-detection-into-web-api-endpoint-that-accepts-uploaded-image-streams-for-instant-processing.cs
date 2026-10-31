// Title: Barcode Generation and Detection from Memory Stream
// Description: Generates a Code128 barcode image in memory and immediately detects it using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the core API classes—BarcodeGenerator for creating barcodes and BarCodeReader for decoding them—from in‑memory streams. Typical use cases include web APIs that accept uploaded image files, process them on the fly, and return decoded information. Developers often need quick, stream‑based barcode handling without persisting temporary files.
// Prompt: Integrate barcode detection into a web API endpoint that accepts uploaded image streams for instant processing.
// Tags: code128, barcode generation, barcode detection, image stream, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode image in memory and detecting it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, saves to a memory stream, and reads it back for detection.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with the data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Prepare an in‑memory stream to hold the generated image
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                ms.Position = 0;

                // Initialize a barcode reader to detect Code128 barcodes from the stream
                using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    bool found = false;
                    // Iterate through all detected barcodes
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        found = true;
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Decoded Text: {result.CodeText}");
                    }
                    // Inform if no barcode was found
                    if (!found)
                    {
                        Console.WriteLine("No barcode detected.");
                    }
                }
            }
        }

        // In a real web API, the input stream would be the uploaded file's stream.
        // This console program demonstrates the core barcode detection logic.
    }
}