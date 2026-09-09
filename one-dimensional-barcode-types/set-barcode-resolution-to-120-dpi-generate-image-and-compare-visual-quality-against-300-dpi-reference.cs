// Title: Barcode resolution comparison between 120 DPI and 300 DPI
// Description: Demonstrates how to set barcode image resolution using Aspose.BarCode, generate PNG images at two DPI settings, and compare their dimensions and file sizes.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator and its Parameters.Resolution property to control output quality. Developers often need to adjust DPI for printing or screen display, and this snippet shows typical steps: configure resolution, save images, and evaluate visual differences. Suitable for searches about barcode DPI settings, image quality comparison, and Aspose.BarCode generation.
// Prompt: Set barcode resolution to 120 DPI, generate image, and compare visual quality against 300 DPI reference.
// Tags: barcode, resolution, dpi, image generation, code128, aspose.barcode, png, comparison

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Generates Code128 barcodes at two different DPI settings (120 and 300) and compares their image dimensions and file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary output folder, generates barcode images, gathers size information, and writes a comparison to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeResolutionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define file paths for low‑ and high‑resolution images
        string lowResPath = Path.Combine(outputFolder, "barcode_120dpi.png");
        string highResPath = Path.Combine(outputFolder, "barcode_300dpi.png");
        string codeText = "1234567890";

        // Generate barcode at 120 DPI
        using (var generatorLow = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generatorLow.Parameters.Resolution = 120f; // Set low resolution
            generatorLow.Save(lowResPath, BarCodeImageFormat.Png);
        }

        // Generate barcode at 300 DPI
        using (var generatorHigh = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generatorHigh.Parameters.Resolution = 300f; // Set high resolution
            generatorHigh.Save(highResPath, BarCodeImageFormat.Png);
        }

        // Load images to obtain dimensions
        int lowWidth, lowHeight, highWidth, highHeight;
        using (var lowBmp = new Bitmap(lowResPath))
        {
            lowWidth = lowBmp.Width;
            lowHeight = lowBmp.Height;
        }
        using (var highBmp = new Bitmap(highResPath))
        {
            highWidth = highBmp.Width;
            highHeight = highBmp.Height;
        }

        // Retrieve file sizes
        long lowSize = new FileInfo(lowResPath).Length;
        long highSize = new FileInfo(highResPath).Length;

        // Output comparison results
        Console.WriteLine("Barcode resolution comparison:");
        Console.WriteLine($"120 DPI image:  {lowWidth}x{lowHeight} pixels, {lowSize} bytes");
        Console.WriteLine($"300 DPI image: {highWidth}x{highHeight} pixels, {highSize} bytes");
        Console.WriteLine("Higher DPI yields larger dimensions and file size, indicating higher visual quality.");
        Console.WriteLine($"Images saved to: {outputFolder}");
    }
}