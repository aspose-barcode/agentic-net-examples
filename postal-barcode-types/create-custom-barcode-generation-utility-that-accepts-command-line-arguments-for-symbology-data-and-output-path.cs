// Title: Command‑Line Barcode Generation Utility
// Description: Demonstrates generating a barcode image using Aspose.BarCode based on command‑line arguments for symbology, data, and output file path.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create barcodes programmatically. Typical use cases include batch barcode creation, integration into build pipelines, or providing a lightweight CLI tool for generating barcodes in various symbologies. Developers often need to resolve symbology names dynamically and ensure proper output handling, which this sample illustrates.
// Prompt: Create a custom barcode generation utility that accepts command‑line arguments for symbology, data, and output path.
// Tags: barcode, symbology, generation, command line, png, aspose.barcode, encode types, barcodegenerator

using System;
using System.IO;
using System.Text;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a simple command‑line utility to generate barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses optional arguments (symbology, data, output path), resolves the symbology,
    /// creates the output directory if needed, and generates a PNG barcode image.
    /// </summary>
    /// <param name="args">Command‑line arguments: [symbology] [data] [outputPath].</param>
    static void Main(string[] args)
    {
        // Default values for symbology, data, and output file
        string symbology = "Code128";
        string data = "123456";
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Override defaults with command‑line arguments if provided
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            symbology = args[0];
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            data = args[1];
        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            outputPath = args[2];

        // Resolve symbology name to BaseEncodeType via reflection (case‑insensitive)
        FieldInfo field = typeof(EncodeTypes).GetField(symbology, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbology}");
            return;
        }

        BaseEncodeType encodeType = field.GetValue(null) as BaseEncodeType;
        if (encodeType == null)
        {
            Console.WriteLine($"Failed to obtain encode type for symbology: {symbology}");
            return;
        }

        // Ensure the output directory exists
        string? dir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // Generate the barcode using the resolved encode type and provided data
        using (var generator = new BarcodeGenerator(encodeType, data))
        {
            // Explicitly set UTF‑8 encoding for the barcode text
            generator.SetCodeText(data, Encoding.UTF8);
            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode generated: {outputPath}");
    }
}