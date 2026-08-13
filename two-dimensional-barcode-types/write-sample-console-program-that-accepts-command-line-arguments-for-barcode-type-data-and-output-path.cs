// Title: Generate Barcode from Command‑Line Arguments
// Description: Demonstrates how to generate a barcode image using Aspose.BarCode by specifying symbology, data, and output path via command‑line arguments.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and related parameter settings to create barcode images. Typical use cases include batch barcode creation, automated report generation, and integration into CI pipelines where image files are needed. Developers often need to map symbology names to EncodeTypes, configure visual properties, and ensure output directories exist.
// Prompt: Write a sample console program that accepts command‑line arguments for barcode type, data, and output path.
// Tags: barcode, symbology, generation, console, command-line, aspose.barcode, aspose.drawing, image, output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Sample console application that generates a barcode image based on command‑line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program.
    /// Accepts optional arguments: symbology name, barcode data, and output file path.
    /// Returns 0 on success, 1 on error.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    /// <returns>Exit code.</returns>
    static int Main(string[] args)
    {
        // Default values for symbology, data, and output path
        string symbologyName = "Code128";
        string codeText = "123456";
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Override defaults with command‑line arguments if they are provided and not empty
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            symbologyName = args[0];
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            codeText = args[1];
        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            outputPath = args[2];

        // Resolve the symbology name to a BaseEncodeType enum value using reflection
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return 1;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Ensure the output directory exists before saving the image
        string? outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate the barcode and save it to the specified path
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Optional: set visual parameters, e.g., barcode color
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Save(outputPath);
        }

        Console.WriteLine($"Barcode generated: {outputPath}");
        return 0;
    }
}