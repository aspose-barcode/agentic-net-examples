// Title: Using Region of Interest to Detect Barcodes in an Image
// Description: Demonstrates how to limit barcode detection to a specific rectangular area of an image using Aspose.BarCode. The example generates a barcode, then reads it from the full image and from a region that does not contain the barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader with a custom region of interest (ROI). It shows how to specify a Rectangle to restrict scanning, a common requirement when processing large images or when only a part of the image is expected to contain barcodes. Developers often need to improve performance or avoid false positives by focusing on a defined area.
// Prompt: Use custom region of interest to limit barcode detection to a specific area of an image.
// Tags: barcode, region of interest, detection, code128, aspose.barcode, barcodereader, rectangle, image processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode detection within a custom region of interest using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, then reads it from the full image and from an empty region.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string imagePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a simple Code128 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the generated image for barcode reading
        using (var bitmap = new Bitmap(imagePath))
        {
            // Define a region that fully contains the barcode (the whole image)
            var fullRegion = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            // Read barcodes within the full region
            using (var readerFull = new BarCodeReader(bitmap, fullRegion, DecodeType.Code128))
            {
                Console.WriteLine("Reading with full region:");
                foreach (BarCodeResult result in readerFull.ReadBarCodes())
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }

            // Define a region that does NOT contain the barcode (top-left corner, small size)
            var emptyRegion = new Rectangle(0, 0, 10, 10);

            // Read barcodes within the empty region
            using (var readerEmpty = new BarCodeReader(bitmap, emptyRegion, DecodeType.Code128))
            {
                Console.WriteLine("Reading with empty region:");
                bool anyFound = false;
                foreach (BarCodeResult result in readerEmpty.ReadBarCodes())
                {
                    anyFound = true;
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
                if (!anyFound)
                {
                    Console.WriteLine("No barcodes detected in the specified region.");
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}