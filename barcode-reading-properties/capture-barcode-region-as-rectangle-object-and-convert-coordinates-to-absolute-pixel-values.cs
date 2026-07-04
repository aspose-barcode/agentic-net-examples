// Title: Capture barcode region as rectangle and convert to absolute pixel coordinates
// Description: Demonstrates generating a Code128 barcode, reading it, and extracting the bounding rectangle in pixel units.
// Prompt: Capture barcode region as a rectangle object and convert coordinates to absolute pixel values.
// Tags: barcode symbology, barcode generation, barcode recognition, rectangle, pixel coordinates, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, recognition, and extraction of the barcode region as pixel coordinates.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, reads it, and prints the barcode type, text, and region rectangle in pixels.
    /// </summary>
    static void Main()
    {
        // Define output image path
        string imagePath = "barcode.png";

        // Create a simple Code128 barcode and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }

        // Load the generated barcode image using Aspose.Drawing
        using (var bitmap = new Bitmap(imagePath))
        {
            // Initialize the reader for all supported barcode types
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Perform recognition and retrieve all detected barcodes
                var results = reader.ReadBarCodes();

                // If no barcodes were detected, inform the user and exit
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                    return;
                }

                // Process each detected barcode
                foreach (var result in results)
                {
                    // Region.Rectangle provides the bounding box in absolute pixel coordinates
                    var rect = result.Region.Rectangle;

                    // Output the barcode details and its region rectangle
                    Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Region (pixels): X={rect.X:F0}, Y={rect.Y:F0}, Width={rect.Width:F0}, Height={rect.Height:F0}");
                }
            }
        }
    }
}