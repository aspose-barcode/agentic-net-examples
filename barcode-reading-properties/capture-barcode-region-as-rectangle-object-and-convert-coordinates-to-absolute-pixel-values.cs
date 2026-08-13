// Title: Capture barcode region as rectangle and convert to absolute pixel coordinates
// Description: Demonstrates generating a Code128 barcode, reading it, and extracting the barcode region as pixel-based rectangle values.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, illustrating how to generate a barcode image with BarcodeGenerator, recognize it using BarCodeReader, and retrieve the Region.Rectangle for each detected barcode. Developers commonly use these APIs to locate barcodes within images, perform layout calculations, or integrate with UI components that require exact pixel positions.
// Prompt: Capture barcode region as a rectangle object and convert coordinates to absolute pixel values.
// Tags: code128, region-capture, pixel-coordinates, barcode-generation, barcode-recognition, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, recognition, and extraction of the barcode region as pixel coordinates.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, reads it, and prints the detected region in absolute pixel values.
    /// </summary>
    static void Main()
    {
        // Create a simple Code128 barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Generate the barcode bitmap (Aspose.Drawing.Bitmap)
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Initialize the reader with the generated bitmap
                using (var reader = new BarCodeReader(bitmap))
                {
                    // Read all barcodes found in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Obtain the region rectangle (coordinates are in pixels)
                        var rect = result.Region.Rectangle;

                        // Convert to absolute integer pixel values
                        int x = (int)Math.Round((double)rect.X);
                        int y = (int)Math.Round((double)rect.Y);
                        int width = (int)Math.Round((double)rect.Width);
                        int height = (int)Math.Round((double)rect.Height);

                        // Output detection details
                        Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                        Console.WriteLine($"Code text: {result.CodeText}");
                        Console.WriteLine($"Region (pixels) - X:{x}, Y:{y}, Width:{width}, Height:{height}");
                    }
                }
            }
        }
    }
}