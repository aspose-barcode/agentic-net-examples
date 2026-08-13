// Title: Specify Rectangular Target Region for Barcode Recognition
// Description: Demonstrates how to limit barcode detection to a defined rectangular area of an image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and related classes to generate a barcode, then restrict recognition to specific regions. Developers often need to focus on a sub‑area of an image to improve performance or avoid false positives, especially when multiple barcodes or visual noise are present.
// Prompt: Specify a rectangular target region before recognition to limit barcode detection to a defined area of the image.
// Tags: code128, region, recognition, barcode, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, saves it as PNG,
/// and demonstrates barcode recognition within specific rectangular regions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image and runs two
    /// recognition scenarios: one with an empty region and one with a region
    /// that fully contains the barcode.
    /// </summary>
    static void Main()
    {
        // Path for the generated barcode image
        string imagePath = "barcode.png";

        // Generate a simple Code128 barcode and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save as PNG format
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Load the generated image into a bitmap for recognition
        using (var bitmap = new Bitmap(imagePath))
        {
            // -------------------------------------------------
            // Example 1: Define a region that does NOT contain the barcode
            // -------------------------------------------------
            // Very small area at the top‑left corner (0,0) with size 10x10 pixels
            var emptyRegion = new Rectangle(0, 0, 10, 10);

            // Initialize the reader with the empty region and specify Code128 decoding
            using (var reader = new BarCodeReader(bitmap, emptyRegion, DecodeType.Code128))
            {
                // Perform recognition within the defined region
                var results = reader.ReadBarCodes();

                // Output the number of barcodes found (expected to be 0)
                Console.WriteLine($"Results in empty region: {reader.FoundCount}");
                foreach (var result in results)
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }

            // -------------------------------------------------
            // Example 2: Define a region that fully contains the barcode
            // -------------------------------------------------
            // Region covering the entire image dimensions
            var fullRegion = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            // Initialize the reader with the full region and specify Code128 decoding
            using (var reader = new BarCodeReader(bitmap, fullRegion, DecodeType.Code128))
            {
                // Perform recognition within the full image area
                var results = reader.ReadBarCodes();

                // Output the number of barcodes found (expected to be 1)
                Console.WriteLine($"Results in full region: {reader.FoundCount}");
                foreach (var result in results)
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }
    }
}