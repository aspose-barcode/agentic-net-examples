// Title: Generate barcode image from command‑line arguments
// Description: Demonstrates how to create a barcode image using Aspose.BarCode by specifying symbology, data, and output path via command‑line parameters.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcode images. Typical use cases include automating barcode creation in batch scripts or CI pipelines where developers need to generate PNG, JPEG, or other formats programmatically.
// Prompt: Write a sample console program that accepts command‑line arguments for barcode type, data, and output path.
// Tags: barcode symbology generation console command-line aspose.barcode

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample console application that generates a barcode image based on command‑line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses arguments, resolves the symbology, ensures the output directory exists, and generates the barcode image.
    /// </summary>
    /// <param name="args">Command‑line arguments: [symbology] [data] [outputPath].</param>
    static void Main(string[] args)
    {
        // Default values used when arguments are not supplied
        string symbologyName = "Code128";
        string data = "12345678";
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Override defaults with provided arguments, if any
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            symbologyName = args[0];
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            data = args[1];
        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            outputPath = args[2];

        // Resolve symbology name to BaseEncodeType via reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        BaseEncodeType encodeType = field.GetValue(null) as BaseEncodeType;
        if (encodeType == null)
        {
            Console.WriteLine($"Failed to obtain encode type for symbology: {symbologyName}");
            return;
        }

        // Ensure the output directory exists before attempting to save
        string? dir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        {
            try
            {
                Directory.CreateDirectory(dir);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to create directory '{dir}': {ex.Message}");
                return;
            }
        }

        try
        {
            // Create the barcode generator with the resolved type and data
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, data))
            {
                // Set a modest XDimension for better visibility
                generator.Parameters.Barcode.XDimension.Pixels = 3f;

                // Save the generated barcode image to the specified path in PNG format
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Barcode generated: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}