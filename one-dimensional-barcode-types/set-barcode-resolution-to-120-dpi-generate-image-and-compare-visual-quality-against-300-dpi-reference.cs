// Title: Barcode resolution comparison between 120 DPI and 300 DPI
// Description: Demonstrates how to set barcode image resolution, generate PNG files, and compare their DPI metadata.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, its Parameters.Resolution property, and image metadata retrieval via Aspose.Drawing. Developers often need to control output resolution for printing or screen display and compare quality across different DPI settings.
// Prompt: Set barcode resolution to 120 DPI, generate image, and compare visual quality against 300 DPI reference.
// Tags: barcode resolution, code128, png, image generation, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates setting barcode resolution to 120 DPI, generating an image, and comparing its visual quality against a 300 DPI reference.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates low‑ and high‑resolution Code128 barcodes, reads their DPI metadata, and outputs a simple quality comparison.
    /// </summary>
    static void Main()
    {
        // Define barcode content and output file names
        const string codeText = "1234567890";
        const string lowResPath = "barcode_120dpi.png";
        const string highResPath = "barcode_300dpi.png";

        // Generate low‑resolution barcode (120 DPI)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Resolution = 120f; // set resolution to 120 DPI
            generator.Save(lowResPath, BarCodeImageFormat.Png);
        }

        // Generate high‑resolution reference barcode (300 DPI)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Resolution = 300f; // set resolution to 300 DPI
            generator.Save(highResPath, BarCodeImageFormat.Png);
        }

        // Load the generated images to read their DPI metadata
        float lowResHorizontal, lowResVertical;
        float highResHorizontal, highResVertical;

        using (var img = Image.FromFile(lowResPath))
        {
            lowResHorizontal = img.HorizontalResolution;
            lowResVertical = img.VerticalResolution;
        }

        using (var img = Image.FromFile(highResPath))
        {
            highResHorizontal = img.HorizontalResolution;
            highResVertical = img.VerticalResolution;
        }

        // Output the resolution values for comparison
        Console.WriteLine($"Low‑resolution image DPI: {lowResHorizontal}x{lowResVertical}");
        Console.WriteLine($"High‑resolution reference DPI: {highResHorizontal}x{highResVertical}");

        // Simple visual quality comparison based on DPI
        if (lowResHorizontal < highResHorizontal && lowResVertical < highResVertical)
        {
            Console.WriteLine("The low‑resolution barcode may appear less sharp than the 300 DPI reference.");
        }
        else
        {
            Console.WriteLine("Resolution comparison could not be performed as expected.");
        }
    }
}