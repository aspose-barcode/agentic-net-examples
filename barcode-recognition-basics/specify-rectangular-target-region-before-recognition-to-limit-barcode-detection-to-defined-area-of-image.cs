// Title: Specify Target Region for Barcode Recognition
// Description: Demonstrates how to limit barcode detection to a rectangular area of an image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of BarCodeReader with a defined target region. It highlights key API classes such as BarcodeGenerator, BarCodeReader, and Rectangle, which are commonly used to generate barcodes, read them, and restrict scanning to specific image sections. Developers often need this pattern when processing large images or focusing on a region of interest to improve performance and accuracy.
// Prompt: Specify a rectangular target region before recognition to limit barcode detection to a defined area of the image.
// Tags: barcode, recognition, region, targetrect, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode, defines a rectangular region,
/// and reads the barcode only within that region using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, sets a target region,
    /// reads barcodes within that region, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "barcode.png");

        // Generate a sample Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Define a rectangular target region (e.g., top-left portion of the image)
        Rectangle targetRect = new Rectangle(0, 0, 200, 100);

        // Load the image and read barcodes only within the specified region
        using (Bitmap bmp = new Bitmap(imagePath))
        {
            using (var reader = new BarCodeReader(bmp, targetRect, DecodeType.Code128))
            {
                Console.WriteLine("Reading with target region:");
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Attempt to delete temporary files and directory; ignore any errors
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Cleanup errors are intentionally ignored
        }
    }
}