// Title: Barcode Generation with Per‑Side Padding for Multiple Symbologies
// Description: Demonstrates how to generate barcodes of different symbologies while applying custom padding values to each side.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, BaseEncodeType, and padding settings. Developers often need to control whitespace around barcodes for layout or printing requirements; this snippet illustrates configuring per‑side padding, XDimension, and saving images in PNG format.
// Prompt: Create a utility that applies different padding values per side for various barcode symbologies in a single workflow.
// Tags: barcode symbology, padding, generation, aspnet, aspose.barcode, png, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating barcodes with custom per‑side padding for several symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode images with specified padding and saves them to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define barcode configurations: symbology, code text, padding per side (points)
        var configs = new[]
        {
            new
            {
                Encode = (BaseEncodeType)EncodeTypes.Code128,
                Text = "CODE128",
                Left = 5f,
                Top = 10f,
                Right = 5f,
                Bottom = 10f,
                FileName = "Code128_Padding.png"
            },
            new
            {
                Encode = (BaseEncodeType)EncodeTypes.QR,
                Text = "QR Code",
                Left = 2f,
                Top = 2f,
                Right = 2f,
                Bottom = 2f,
                FileName = "QR_Padding.png"
            },
            new
            {
                Encode = (BaseEncodeType)EncodeTypes.DataMatrix,
                Text = "DMATRIX",
                Left = 8f,
                Top = 4f,
                Right = 8f,
                Bottom = 4f,
                FileName = "DataMatrix_Padding.png"
            },
            new
            {
                Encode = (BaseEncodeType)EncodeTypes.Pdf417,
                Text = "PDF417 Sample",
                Left = 12f,
                Top = 6f,
                Right = 12f,
                Bottom = 6f,
                FileName = "Pdf417_Padding.png"
            }
        };

        // Iterate over each configuration and generate the corresponding barcode
        foreach (var cfg in configs)
        {
            try
            {
                using (BarcodeGenerator generator = new BarcodeGenerator(cfg.Encode, cfg.Text))
                {
                    // Set padding per side (points)
                    generator.Parameters.Barcode.Padding.Left.Point = cfg.Left;
                    generator.Parameters.Barcode.Padding.Top.Point = cfg.Top;
                    generator.Parameters.Barcode.Padding.Right.Point = cfg.Right;
                    generator.Parameters.Barcode.Padding.Bottom.Point = cfg.Bottom;

                    // Optional: set XDimension for better visibility
                    generator.Parameters.Barcode.XDimension.Point = 2f;

                    // Build the full output path and save the image as PNG
                    string outputPath = Path.Combine(outputFolder, cfg.FileName);
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Saved {cfg.FileName} with custom padding to {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during generation
                Console.WriteLine($"Error generating {cfg.FileName}: {ex.Message}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}