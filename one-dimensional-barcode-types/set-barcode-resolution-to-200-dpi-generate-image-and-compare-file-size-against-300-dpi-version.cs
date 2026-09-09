// Title: Barcode resolution comparison between 200 DPI and 300 DPI
// Description: Demonstrates how to set barcode image resolution using Aspose.BarCode, generate PNG images at 200 DPI and 300 DPI, and compare their file sizes.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to control image resolution. Developers often need to adjust DPI for printing quality or file size optimization, and this snippet shows typical steps for creating barcodes at different resolutions and evaluating the impact on output size.
// Prompt: Set barcode resolution to 200 DPI, generate image, and compare file size against 300 DPI version.
// Tags: barcode, resolution, dpi, image generation, png, aspose.barcode, code128, file size comparison

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates setting barcode resolution, generating PNG images at different DPI values,
/// and comparing the resulting file sizes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary output folder, generates two barcodes
    /// (200 DPI and 300 DPI), and prints a size comparison to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeResolutionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Barcode data and target file paths
        string codeText = "1234567890";
        string file200 = Path.Combine(outputDir, "barcode_200dpi.png");
        string file300 = Path.Combine(outputDir, "barcode_300dpi.png");

        // Generate a 200 DPI barcode image
        using (var generator200 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator200.Parameters.Resolution = 200f; // Set resolution to 200 DPI
            generator200.Save(file200, BarCodeImageFormat.Png); // Save as PNG
        }

        // Generate a 300 DPI barcode image
        using (var generator300 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator300.Parameters.Resolution = 300f; // Set resolution to 300 DPI
            generator300.Save(file300, BarCodeImageFormat.Png); // Save as PNG
        }

        // Retrieve file sizes for both images
        long size200 = new FileInfo(file200).Length;
        long size300 = new FileInfo(file300).Length;

        // Output the sizes to the console
        Console.WriteLine($"200 DPI file size: {size200} bytes");
        Console.WriteLine($"300 DPI file size: {size300} bytes");

        // Compare and report which file is larger
        if (size200 < size300)
            Console.WriteLine("200 DPI file is smaller than 300 DPI file.");
        else if (size200 > size300)
            Console.WriteLine("200 DPI file is larger than 300 DPI file.");
        else
            Console.WriteLine("Both files have the same size.");
    }
}