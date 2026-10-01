// Title: Demonstrate changing BarCodeReader source image with SetBarCodeImage
// Description: This example generates two barcode images, reads the first, then switches the reader to the second image using SetBarCodeImage, showing how to reuse a BarCodeReader instance.
// Category-Description: Aspose.BarCode example illustrating dynamic source image replacement for BarCodeReader. It covers barcode generation (BarcodeGenerator), image saving (BarCodeImageFormat), and barcode recognition (BarCodeReader). Developers working with multiple barcode images can reuse a single reader to improve performance and simplify code.
// Prompt: Change the source image after construction by calling SetBarCodeImage with a new file path.
// Tags: barcode symbology, set image, read, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating barcode images, reading them, and swapping the source image of a BarCodeReader at runtime.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates two barcodes, reads the first, then switches to the second image using SetBarCodeImage.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the two barcode images
        string firstImagePath = Path.Combine(tempFolder, "barcode1.png");
        string secondImagePath = Path.Combine(tempFolder, "barcode2.png");

        // Generate the first barcode image (Code128) and save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "First123"))
        {
            generator.Save(firstImagePath, BarCodeImageFormat.Png);
        }

        // Generate the second barcode image (QR) and save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Second456"))
        {
            generator.Save(secondImagePath, BarCodeImageFormat.Png);
        }

        // Verify that the first image exists before creating the reader
        if (!File.Exists(firstImagePath))
        {
            Console.WriteLine("First barcode image not found: " + firstImagePath);
            return;
        }

        // Initialize BarCodeReader with the first image
        using (var reader = new BarCodeReader(firstImagePath))
        {
            Console.WriteLine("Reading barcodes from first image:");
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // Ensure the second image exists before swapping
            if (!File.Exists(secondImagePath))
            {
                Console.WriteLine("Second barcode image not found: " + secondImagePath);
                return;
            }

            // Change the source image of the existing reader to the second image
            reader.SetBarCodeImage(secondImagePath);

            Console.WriteLine("Reading barcodes after SetBarCodeImage to second image:");
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(firstImagePath);
            File.Delete(secondImagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect demo execution
        }
    }
}