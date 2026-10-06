// Title: Command‑Line Barcode Generation Utility
// Description: Demonstrates generating a barcode image using Aspose.BarCode based on command‑line parameters for symbology, data, and output file path.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class together with EncodeTypes to create barcodes programmatically. Typical use cases include batch barcode creation, integration into build pipelines, or providing a lightweight CLI tool for generating barcodes in various formats. Developers often need to map user‑friendly symbology names to EncodeTypes, configure basic parameters, and handle file system concerns.
// Prompt: Create a custom barcode generation utility that accepts command‑line arguments for symbology, data, and output path.
// Tags: barcode, symbology, command-line, generation, aspose.barcode, png, encode-types

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple command‑line utility for generating barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Parses command‑line arguments, resolves the requested symbology,
    /// ensures the output directory exists, and generates the barcode image.
    /// </summary>
    /// <param name="args">
    /// Expected arguments:
    /// 0 – Symbology name (e.g., "Code128").
    /// 1 – Data to encode.
    /// 2 – Output file path (including file name and extension).
    /// </param>
    static void Main(string[] args)
    {
        // Default values used when arguments are missing or empty
        string symbologyName = "Code128";
        string data = "123456";
        string outputPath = "barcode.png";

        // Override defaults with supplied arguments, if any
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            symbologyName = args[0];
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            data = args[1];
        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            outputPath = args[2];

        // Resolve the symbology name to a BaseEncodeType enum value using reflection
        var field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
        if (encodeType == null)
        {
            Console.WriteLine($"Failed to obtain encode type for symbology: {symbologyName}");
            return;
        }

        // Ensure the directory for the output file exists
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            try
            {
                Directory.CreateDirectory(directory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to create output directory: {ex.Message}");
                return;
            }
        }

        // Create the barcode generator with the resolved encode type and data
        using (var generator = new BarcodeGenerator(encodeType, data))
        {
            // Optional: set a modest X dimension for better visual quality
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            try
            {
                // Save the generated barcode to the specified path in PNG format
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode: {ex.Message}");
            }
        }
    }
}