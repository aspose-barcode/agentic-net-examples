// Title: Barcode Generation with Per‑Side Padding for Multiple Symbologies
// Description: Demonstrates how to generate barcodes of different symbologies while applying individual padding values for each side.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create images with custom padding. Developers often need to adjust whitespace around barcodes for layout or printing requirements, especially when handling multiple symbologies in a single workflow. The snippet illustrates typical usage patterns for setting per‑side padding, X‑dimension, and saving PNG output.
// Prompt: Create a utility that applies different padding values per side for various barcode symbologies in a single workflow.
// Tags: barcode symbology, padding, generation, aspose.barcode, png, encode types

using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates barcode images for several symbologies, applying distinct padding values per side.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates an output folder, defines barcode specifications,
    /// generates each barcode with custom padding, and saves the images as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output folder
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define barcode specifications:
        // Symbology name, code text, and padding values (left, top, right, bottom) in pixels
        var barcodeSpecs = new List<(string Symbology, string CodeText, float Left, float Top, float Right, float Bottom)>
        {
            ("Code128", "ASPOSE123", 10f, 5f, 10f, 5f),
            ("QR", "https://www.aspose.com", 2f, 2f, 2f, 2f),
            ("DataMatrix", "DM12345", 0f, 0f, 0f, 0f),
            ("Pdf417", "PDF417 Sample Text", 8f, 12f, 8f, 12f),
            ("Aztec", "AztecDemo", 4f, 6f, 4f, 6f)
        };

        // Iterate over each specification and generate the corresponding barcode
        foreach (var spec in barcodeSpecs)
        {
            // Resolve the symbology name to a BaseEncodeType enum value using reflection
            FieldInfo field = typeof(EncodeTypes).GetField(spec.Symbology);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology: {spec.Symbology}");
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
            string fileName = $"{spec.Symbology}_{Guid.NewGuid().ToString("N")}.png";
            string filePath = Path.Combine(outputFolder, fileName);

            // Initialize the barcode generator with the resolved type and code text
            using (var generator = new BarcodeGenerator(encodeType, spec.CodeText))
            {
                // Apply per‑side padding (pixels)
                generator.Parameters.Barcode.Padding.Left.Pixels = spec.Left;
                generator.Parameters.Barcode.Padding.Top.Pixels = spec.Top;
                generator.Parameters.Barcode.Padding.Right.Pixels = spec.Right;
                generator.Parameters.Barcode.Padding.Bottom.Pixels = spec.Bottom;

                // Optional: set a modest XDimension for better visibility
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Save the generated barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated {spec.Symbology} barcode at: {filePath}");
        }

        Console.WriteLine("Barcode generation completed.");
    }
}