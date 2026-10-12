// Title: Limit barcode recognition to a specific rectangular region
// Description: Demonstrates how to define a target rectangle on an image so that barcode detection is performed only within that area, improving performance and accuracy.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, illustrating the use of BarCodeGenerator for creating barcodes and BarCodeReader with a specified Rectangle to limit detection. Developers often need to focus recognition on a region of interest in larger images, such as scanning a label within a photo, and this pattern shows the typical API usage for that scenario.
// Prompt: Specify a rectangular target region before recognition to limit barcode detection to a defined area of the image.
// Tags: qr, region, detection, png, barcodegenerator, barcodeimageformat, bitmap, barcodereader, rectangle

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a QR code, defines a target region, and reads the barcode only within that region.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "barcode.png");

        // Generate a QR code image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Define a target region (top-left corner of the image) where recognition will be performed
        var targetRegion = new Rectangle(0, 0, 200, 200);

        // Load the image and read barcodes within the specified region
        using (var bitmap = new Bitmap(imagePath))
        {
            using (var reader = new BarCodeReader(bitmap, targetRegion, DecodeType.QR))
            {
                Console.WriteLine("Reading with target region:");
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}