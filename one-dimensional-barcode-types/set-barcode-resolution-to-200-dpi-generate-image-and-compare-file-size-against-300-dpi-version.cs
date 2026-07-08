// Title: Barcode resolution comparison between 200 DPI and 300 DPI
// Description: Demonstrates how to set barcode image resolution using Aspose.BarCode, generate PNG images at two DPI settings, and compare their file sizes.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers often need to control image resolution for printing or display quality, and compare resulting file sizes to optimize storage or performance.
// Prompt: Set barcode resolution to 200 DPI, generate image, and compare file size against 300 DPI version.
// Tags: barcode, resolution, png, code128, image-generation, file-size-comparison

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Code128 barcodes at two different resolutions (200 DPI and 300 DPI),
/// saves them as PNG files, and compares the resulting file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode images, verifies their existence,
    /// and outputs a size comparison to the console.
    /// </summary>
    static void Main()
    {
        // Define barcode content and output file names
        const string barcodeText = "123456789";
        string file200 = "barcode_200dpi.png";
        string file300 = "barcode_300dpi.png";

        // Generate barcode image at 200 DPI
        using (var generator200 = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            generator200.Parameters.Resolution = 200f; // set resolution to 200 DPI
            generator200.Save(file200, BarCodeImageFormat.Png);
        }

        // Generate barcode image at 300 DPI
        using (var generator300 = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            generator300.Parameters.Resolution = 300f; // set resolution to 300 DPI
            generator300.Save(file300, BarCodeImageFormat.Png);
        }

        // Verify that both files were successfully created
        if (!File.Exists(file200) || !File.Exists(file300))
        {
            Console.WriteLine("Failed to generate one or both barcode images.");
            return;
        }

        // Retrieve file sizes in bytes
        long size200 = new FileInfo(file200).Length;
        long size300 = new FileInfo(file300).Length;

        // Display file sizes
        Console.WriteLine($"200 DPI file size: {size200} bytes");
        Console.WriteLine($"300 DPI file size: {size300} bytes");

        // Compare and report which image is larger
        if (size200 == size300)
        {
            Console.WriteLine("Both images have the same file size.");
        }
        else if (size200 > size300)
        {
            Console.WriteLine("200 DPI image is larger than 300 DPI image.");
        }
        else
        {
            Console.WriteLine("300 DPI image is larger than 200 DPI image.");
        }
    }
}