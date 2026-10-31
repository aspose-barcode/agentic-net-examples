// Title: Barcode Generation with Per‑Side Padding for Multiple Symbologies
// Description: Demonstrates how to generate barcodes of different symbologies while applying distinct padding values to each side of the image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and the Padding properties to customize barcode appearance. Typical use cases include creating printable labels, receipts, or UI assets where precise margin control is required. Developers often need to adjust per‑side padding to fit layout constraints or visual design guidelines.
// Prompt: Create a utility that applies different padding values per side for various barcode symbologies in a single workflow.
// Tags: barcode symbology, padding, generation, aspose.barcode, png, c#
using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcodes with per‑side padding for various symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates an output folder, configures barcode settings, generates images, and writes paths to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated barcode images
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define a list of barcode configurations, each with its own per‑side padding values
        var barcodeConfigs = new List<(BaseEncodeType type, string text, float left, float top, float right, float bottom)>
        {
            (EncodeTypes.Code128, "CODE128", 5f, 10f, 15f, 20f),
            (EncodeTypes.QR, "QRCODE", 2f, 4f, 6f, 8f),
            (EncodeTypes.DataMatrix, "DMATRIX", 3f, 3f, 3f, 3f),
            (EncodeTypes.Pdf417, "PDF417DATA", 0f, 5f, 10f, 15f),
            (EncodeTypes.Interleaved2of5, "1234567890", 1f, 2f, 3f, 4f)
        };

        // Iterate over each configuration, generate the barcode, and save it as a PNG file
        foreach (var config in barcodeConfigs)
        {
            // Build a descriptive file name using the symbology type and encoded text
            string fileName = $"{config.type.GetType().Name}_{config.text}.png";
            string filePath = Path.Combine(outputFolder, fileName);

            // Initialize the barcode generator with the specified type and text
            using (var generator = new BarcodeGenerator(config.type, config.text))
            {
                // Set a modest XDimension (module width) for better visual density
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Apply the per‑side padding values defined in the configuration
                generator.Parameters.Barcode.Padding.Left.Point = config.left;
                generator.Parameters.Barcode.Padding.Top.Point = config.top;
                generator.Parameters.Barcode.Padding.Right.Point = config.right;
                generator.Parameters.Barcode.Padding.Bottom.Point = config.bottom;

                // Save the generated barcode image to the output folder in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output the location of the generated file for verification
            Console.WriteLine($"Generated {filePath}");
        }

        // Indicate that the batch processing has completed
        Console.WriteLine("All barcodes have been generated.");
    }
}