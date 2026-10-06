// Title: Custom Region of Interest for Barcode Detection
// Description: Demonstrates how to limit barcode recognition to a specific area of an image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on region‑of‑interest (ROI) techniques. It showcases the BarCodeReader class with overloads that accept a Rectangle defining the ROI, a common requirement when processing large images or when only a portion of an image contains barcodes. Developers often use this pattern to improve performance and accuracy in scenarios such as document scanning, industrial automation, and mobile capture.
// Prompt: Use custom region of interest to limit barcode detection to a specific area of an image.
// Tags: barcode, region of interest, detection, code128, aspose.barcode, csharp, image processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program showing how to use a custom region of interest to limit barcode detection.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image, then reads it using full and limited regions to illustrate ROI functionality.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder for the demo files
        string workFolder = Path.Combine(Path.GetTempPath(), "BarRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(workFolder, "barcode.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the barcode image into a bitmap for processing
        using (Bitmap bitmap = new Bitmap(barcodePath))
        {
            // Define a region that fully contains the barcode (the whole image)
            Rectangle fullRegion = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            // Define a region that does NOT contain the barcode (top‑left corner, small area)
            Rectangle emptyRegion = new Rectangle(0, 0, 10, 10);

            // Read using the full region – the barcode should be detected
            Console.WriteLine("Reading with full region:");
            using (var readerFull = new BarCodeReader(bitmap, fullRegion, DecodeType.Code128))
            {
                foreach (BarCodeResult result in readerFull.ReadBarCodes())
                {
                    Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                }
            }

            // Read using the empty region – no barcode should be detected
            Console.WriteLine("Reading with empty region:");
            using (var readerEmpty = new BarCodeReader(bitmap, emptyRegion, DecodeType.Code128))
            {
                bool any = false;
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
            File.Delete(barcodePath);
            Directory.Delete(workFolder, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program outcome
        }
    }
}