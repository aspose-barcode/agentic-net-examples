// Title: Custom Barcode Generation Utility
// Description: Demonstrates generating a barcode image using Aspose.BarCode based on command‑line parameters for symbology, data, and output file path.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes to create various barcode symbologies. Typical use cases include automating barcode creation in batch scripts, CI pipelines, or desktop utilities where users specify the barcode type and content at runtime. Developers often need to resolve symbology names dynamically, handle output directories, and manage exceptions during image generation.
// Prompt: Create a custom barcode generation utility that accepts command‑line arguments for symbology, data, and output path.
// Tags: barcode, symbology, generation, png, aspose.barcodes, encode types

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a command‑line utility to generate barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the utility. Parses command‑line arguments, resolves the requested symbology,
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
        // Default values used when arguments are not supplied
        string symbologyName = "Code128";
        string data = "123456";
        string outputPath = "barcode.png";

        // Override defaults with provided command‑line arguments, if any
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            symbologyName = args[0];
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            data = args[1];
        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            outputPath = args[2];

        // Resolve the symbology name to an EncodeTypes field using reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        // Cast the reflected field value to BaseEncodeType
        BaseEncodeType encodeType = field.GetValue(null) as BaseEncodeType;
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

        // Generate the barcode and save it to the specified path
        try
        {
            using (var generator = new BarcodeGenerator(encodeType, data))
            {
                generator.Save(outputPath);
            }

            Console.WriteLine($"Barcode generated successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}