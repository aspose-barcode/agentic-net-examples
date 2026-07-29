// Title: Apply per-side padding to multiple barcode symbologies
// Description: Demonstrates how to set individual padding values for each side of various barcode types using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and padding parameters. It helps developers who need fine‑grained control over barcode margins for different symbologies, such as Code128, QR, DataMatrix, PDF417, and GS1 DataBar, and want to output images in common formats.
// Prompt: Create a utility that applies different padding values per side for various barcode symbologies in a single workflow.
// Tags: barcode symbology, padding, generation, png, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a set of barcodes with custom per‑side padding and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the utility. Creates an output folder, defines barcode configurations,
    /// applies side‑specific padding, and saves each barcode image.
    /// </summary>
    static void Main()
    {
        // Create (or reuse) the output directory for generated barcode images.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputFolder);

        // Define a collection of barcode configurations, each with its own type, text, padding, and file name.
        var configs = new[]
        {
            new
            {
                Type = (BaseEncodeType)EncodeTypes.Code128,
                CodeText = "CODE128_SAMPLE",
                Padding = new { Left = 5f, Top = 10f, Right = 5f, Bottom = 10f },
                FileName = "Code128.png"
            },
            new
            {
                Type = (BaseEncodeType)EncodeTypes.QR,
                CodeText = "QR_SAMPLE",
                Padding = new { Left = 2f, Top = 2f, Right = 2f, Bottom = 2f },
                FileName = "QR.png"
            },
            new
            {
                Type = (BaseEncodeType)EncodeTypes.DataMatrix,
                CodeText = "DM_SAMPLE",
                Padding = new { Left = 0f, Top = 0f, Right = 0f, Bottom = 0f },
                FileName = "DataMatrix.png"
            },
            new
            {
                Type = (BaseEncodeType)EncodeTypes.Pdf417,
                CodeText = "PDF417_SAMPLE",
                Padding = new { Left = 8f, Top = 4f, Right = 8f, Bottom = 4f },
                FileName = "Pdf417.png"
            },
            new
            {
                Type = (BaseEncodeType)EncodeTypes.DatabarStacked,
                CodeText = "(01)01234567890123",
                Padding = new { Left = 3f, Top = 6f, Right = 3f, Bottom = 6f },
                FileName = "DataBarStacked.png"
            }
        };

        // Iterate over each configuration, generate the barcode, apply padding, and save the image.
        foreach (var cfg in configs)
        {
            string outputPath = Path.Combine(outputFolder, cfg.FileName);
            using (var generator = new BarcodeGenerator(cfg.Type, cfg.CodeText))
            {
                // Apply per‑side padding (values are specified in points).
                generator.Parameters.Barcode.Padding.Left.Point = cfg.Padding.Left;
                generator.Parameters.Barcode.Padding.Top.Point = cfg.Padding.Top;
                generator.Parameters.Barcode.Padding.Right.Point = cfg.Padding.Right;
                generator.Parameters.Barcode.Padding.Bottom.Point = cfg.Padding.Bottom;

                // Set the bar color to black (optional visual customization).
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Save the generated barcode as a PNG image.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved {cfg.FileName} with custom padding to {outputPath}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}