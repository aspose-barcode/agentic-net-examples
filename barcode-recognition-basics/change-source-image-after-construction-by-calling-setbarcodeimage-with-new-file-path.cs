// Title: Change barcode source image after construction using SetBarCodeImage
// Description: Demonstrates how to generate two barcodes, then read them using a single BarCodeReader instance by switching the source image with SetBarCodeImage. Shows practical use of reusing a reader for multiple images.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It illustrates the use of BarcodeGenerator to create barcode images and BarCodeReader to decode them, highlighting the SetBarCodeImage method for changing the input image without recreating the reader. Developers working with batch barcode scanning or dynamic image sources often need to reuse a reader instance to improve performance.
// Prompt: Change the source image after construction by calling SetBarCodeImage with a new file path.
// Tags: barcode generation, barcode recognition, setbarcodeimage, code128, qr, aspnet, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates two barcode images and reads them using a single
/// <see cref="BarCodeReader"/> instance, switching the source image via <c>SetBarCodeImage</c>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, reads them, and cleans up temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a temporary folder for sample images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the two barcode images
        string firstImagePath = Path.Combine(tempFolder, "code128.png");
        string secondImagePath = Path.Combine(tempFolder, "qr.png");

        // Generate the first barcode (Code128) and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(firstImagePath, BarCodeImageFormat.Png);
        }

        // Generate the second barcode (QR) and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "HelloWorld"))
        {
            generator.Save(secondImagePath, BarCodeImageFormat.Png);
        }

        // Initialize a single BarCodeReader instance
        using (var reader = new BarCodeReader())
        {
            // Set the first image as the source and configure the reader for Code128
            reader.SetBarCodeImage(firstImagePath);
            reader.SetBarCodeReadType(DecodeType.Code128);
            Console.WriteLine("Reading first image:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
            }

            // Switch to the second image and configure the reader for QR
            reader.SetBarCodeImage(secondImagePath);
            reader.SetBarCodeReadType(DecodeType.QR);
            Console.WriteLine("Reading second image:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
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
            // Ignore any cleanup errors
        }
    }
}