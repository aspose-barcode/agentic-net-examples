// Title: Barcode resolution comparison between 120 DPI and 300 DPI
// Description: Demonstrates how to set barcode image resolution using Aspose.BarCode, generate PNG images at 120 DPI and 300 DPI, and compare their dimensions and file sizes to assess visual quality.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, its Parameters.Resolution property, and BarCodeImageFormat for creating high‑resolution barcode graphics. Typical scenarios include printing barcodes on labels, packaging, or documents where visual clarity matters. Developers often need to adjust DPI to meet printing standards or to balance file size against quality.
// Prompt: Set barcode resolution to 120 DPI, generate image, and compare visual quality against 300 DPI reference.
// Tags: barcode, resolution, datamatrix, image generation, png, aspose.barcode, aspose.drawing, comparison

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates DataMatrix barcodes at two different DPI settings (120 DPI and 300 DPI),
/// saves them as PNG files, and compares their dimensions and file sizes to illustrate
/// the impact of resolution on visual quality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Creates temporary files, generates barcodes,
    /// outputs comparison data, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeResolutionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Barcode content and target file paths
        string codeText = "ASPOSE";
        string file120 = Path.Combine(tempDir, "barcode_120dpi.png");
        string file300 = Path.Combine(tempDir, "barcode_300dpi.png");

        // Generate a 120 DPI DataMatrix barcode and save as PNG
        using (var generator120 = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            generator120.Parameters.Resolution = 120f; // Set low resolution
            generator120.Save(file120, BarCodeImageFormat.Png);
        }

        // Generate a 300 DPI DataMatrix barcode and save as PNG
        using (var generator300 = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            generator300.Parameters.Resolution = 300f; // Set high resolution
            generator300.Save(file300, BarCodeImageFormat.Png);
        }

        // Load the generated images to compare pixel dimensions and file sizes
        using (var img120 = new Bitmap(file120))
        using (var img300 = new Bitmap(file300))
        {
            Console.WriteLine($"120 DPI image:  {img120.Width}x{img120.Height} pixels, {new FileInfo(file120).Length} bytes");
            Console.WriteLine($"300 DPI image: {img300.Width}x{img300.Height} pixels, {new FileInfo(file300).Length} bytes");

            // Compare dimensions
            if (img120.Width == img300.Width && img120.Height == img300.Height)
            {
                Console.WriteLine("Dimensions are identical.");
            }
            else
            {
                Console.WriteLine("Dimensions differ due to resolution setting.");
            }

            // Compare file sizes as a proxy for visual quality
            if (new FileInfo(file120).Length < new FileInfo(file300).Length)
            {
                Console.WriteLine("Higher DPI image has larger file size, indicating higher visual quality.");
            }
            else
            {
                Console.WriteLine("Unexpected file size relationship.");
            }
        }

        // Optional cleanup of temporary files and directory
        try
        {
            File.Delete(file120);
            File.Delete(file300);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Suppress any cleanup errors (e.g., files in use)
        }
    }
}