// Title: Change barcode source image using SetBarCodeImage
// Description: Generates two barcode images (Code128 and QR) and demonstrates how to switch the source image of a BarCodeReader at runtime by calling SetBarCodeImage with a new file path.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcode images and BarCodeReader to decode them. Developers often need to process multiple images with a single reader instance; SetBarCodeImage enables reusing the same BarCodeReader object for different source files, improving performance and resource management.
// Prompt: Change the source image after construction by calling SetBarCodeImage with a new file path.
// Tags: barcode generation, barcode recognition, setbarcodeimage, code128, qr, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating barcode images and reading them by dynamically changing the source image of a BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, reads them using a single BarCodeReader,
    /// and switches the source image via SetBarCodeImage.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "BarCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the two sample images
        string imgPath1 = Path.Combine(tempDir, "code128.png");
        string imgPath2 = Path.Combine(tempDir, "qr.png");

        // Generate first barcode (Code128) and save as PNG
        using (var generator1 = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator1.Save(imgPath1, BarCodeImageFormat.Png);
        }

        // Generate second barcode (QR) and save as PNG
        using (var generator2 = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator2.Save(imgPath2, BarCodeImageFormat.Png);
        }

        // Verify that both image files were created successfully
        if (!File.Exists(imgPath1) || !File.Exists(imgPath2))
        {
            Console.WriteLine("Failed to create sample barcode images.");
            return;
        }

        // Initialize a BarCodeReader without a source image
        using (var reader = new BarCodeReader())
        {
            // Set the first image as the source and read its barcodes
            reader.SetBarCodeImage(imgPath1);
            Console.WriteLine("Reading first image:");
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Change the source to the second image and read its barcodes
            reader.SetBarCodeImage(imgPath2);
            Console.WriteLine("Reading second image:");
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Cleanup temporary files and folder
        try { File.Delete(imgPath1); } catch { }
        try { File.Delete(imgPath2); } catch { }
        try { Directory.Delete(tempDir, true); } catch { }
    }
}