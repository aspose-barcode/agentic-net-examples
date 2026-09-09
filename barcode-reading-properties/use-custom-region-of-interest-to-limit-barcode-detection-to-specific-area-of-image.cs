// Title: Barcode detection with custom region of interest
// Description: Demonstrates how to limit barcode recognition to a specific rectangular area of an image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader with a defined region of interest. It highlights key API classes such as BarcodeGenerator, BarCodeReader, and DecodeType, which are commonly used for generating barcodes, detecting them in images, and specifying decoding parameters. Developers often need to focus detection on a particular area to improve performance or avoid false positives, making this pattern useful in image processing pipelines.
// Prompt: Use custom region of interest to limit barcode detection to a specific area of an image.
// Tags: barcode, code128, region of interest, detection, aspose.barcode, image processing, barcode generation, barcode recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates using a custom region of interest to limit barcode detection in an image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, then reads it using full and limited regions.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder for generated files
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Path for the barcode image file
        string imagePath = Path.Combine(workFolder, "barcode.png");

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

        // Load the generated image for recognition
        using (var bitmap = new Bitmap(imagePath))
        {
            // Define a region that covers the whole image (should detect the barcode)
            var fullRegion = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            Console.WriteLine("Reading with full-region:");
            using (var readerFull = new BarCodeReader(bitmap, fullRegion, DecodeType.Code128))
            {
                // Iterate through all detected barcodes in the full region
                foreach (BarCodeResult result in readerFull.ReadBarCodes())
                {
                    Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                }
            }

            // Define a region that does NOT include the barcode (top-left corner 10x10)
            var emptyRegion = new Rectangle(0, 0, 10, 10);

            Console.WriteLine("Reading with empty-region:");
            using (var readerEmpty = new BarCodeReader(bitmap, emptyRegion, DecodeType.Code128))
            {
                bool any = false;
                // Attempt to read barcodes in the limited region
                foreach (BarCodeResult result in readerEmpty.ReadBarCodes())
                {
                    any = true;
                    Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                }
                if (!any)
                {
                    Console.WriteLine("No barcodes detected in the specified region.");
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(imagePath);
            Directory.Delete(workFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}