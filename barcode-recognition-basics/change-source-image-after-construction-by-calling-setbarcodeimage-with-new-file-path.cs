// Title: Change barcode source image using SetBarCodeImage
// Description: Generates two barcode images and reads them with a single BarCodeReader, switching the source image between reads via SetBarCodeImage.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes and the BarCodeReader for decoding them. Developers often need to process multiple images efficiently; reusing a BarCodeReader instance and changing its source image with SetBarCodeImage reduces overhead and simplifies code.
// Prompt: Change the source image after construction by calling SetBarCodeImage with a new file path.
// Tags: barcode generation, barcode recognition, setbarcodeimage, code128, qr, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to change the source image of a BarCodeReader after construction using SetBarCodeImage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcode images, reads them with a single BarCodeReader, and switches the source image between reads.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the two barcode images
        string firstImagePath = Path.Combine(tempFolder, "first.png");
        string secondImagePath = Path.Combine(tempFolder, "second.png");

        // Generate the first barcode (Code128) and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "First"))
        {
            generator.Save(firstImagePath, BarCodeImageFormat.Png);
        }

        // Generate the second barcode (QR) and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Second"))
        {
            generator.Save(secondImagePath, BarCodeImageFormat.Png);
        }

        // Verify that both image files were created successfully
        if (!File.Exists(firstImagePath) || !File.Exists(secondImagePath))
        {
            Console.WriteLine("Failed to create barcode images.");
            return;
        }

        // Use a single BarCodeReader instance to read both images
        using (var reader = new BarCodeReader())
        {
            // Set the source to the first image and read its barcodes
            reader.SetBarCodeImage(firstImagePath);
            Console.WriteLine("Reading first image:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Change the source to the second image and read its barcodes
            reader.SetBarCodeImage(secondImagePath);
            Console.WriteLine("Reading second image:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Cleanup temporary files (optional)
        try
        {
            File.Delete(firstImagePath);
            File.Delete(secondImagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}