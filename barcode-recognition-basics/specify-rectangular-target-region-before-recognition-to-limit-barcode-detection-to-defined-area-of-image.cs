// Title: Barcode detection within a specified rectangular region
// Description: Demonstrates how to limit barcode recognition to a defined area of an image by specifying a target rectangle before scanning.
// Prompt: Specify a rectangular target region before recognition to limit barcode detection to a defined area of the image.
// Tags: barcode, code128, region, recognition, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, defines a target region,
/// and reads barcodes only within that region.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, sets a rectangular target region,
    /// and uses BarCodeReader to detect barcodes confined to that region.
    /// </summary>
    static void Main()
    {
        // Generate a sample Code128 barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Set image size using point units (300x150 points)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Create the barcode bitmap
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Define a rectangular region (top‑left quarter of the image)
                var targetRegion = new Rectangle(0, 0, bitmap.Width / 2, bitmap.Height / 2);

                // Initialize a BarCodeReader that scans only within the specified region
                using (var reader = new BarCodeReader(bitmap, targetRegion, DecodeType.Code128))
                {
                    // Iterate through all detected barcodes in the region
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");

                        // Retrieve and display the bounding rectangle of the detected barcode
                        var bounds = result.Region.Rectangle;
                        Console.WriteLine($"Barcode Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}");
                    }
                }
            }
        }
    }
}