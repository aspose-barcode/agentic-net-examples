// Title: Command‑line Barcode Generator Utility
// Description: Demonstrates generating a barcode image with Aspose.BarCode using command‑line arguments for symbology, data, and output file path.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create barcodes programmatically. It uses the EncodeTypes enumeration to select a symbology, the BarcodeGenerator class to produce the image, and BarCodeImageFormat for output. Typical scenarios include batch barcode creation, integration into build pipelines, or providing a lightweight CLI tool for developers who need to generate barcodes on demand.
/// Prompt: Create a custom barcode generation utility that accepts command‑line arguments for symbology, data, and output path.
/// Tags: barcode, symbology, generation, command-line, aspose.barcode, encode-types, barcodegenerator

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Simple console application that generates a barcode image based on command‑line parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses arguments, resolves the requested symbology, and saves the generated barcode.
    /// </summary>
    /// <param name="args">
    /// Expected arguments:
    /// 0 – Symbology name (e.g., "Code128").
    /// 1 – Data to encode.
    /// 2 – Output file path (including file name and extension).
    /// </param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Set default values that will be used when corresponding arguments are missing.
        // --------------------------------------------------------------------
        string symbologyName = "Code128";
        string data = "Sample123";
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // --------------------------------------------------------------------
        // Override defaults with supplied command‑line arguments, if any.
        // --------------------------------------------------------------------
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            symbologyName = args[0];
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            data = args[1];
        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            outputPath = args[2];

        // --------------------------------------------------------------------
        // Ensure the directory for the output file exists; create it if necessary.
        // --------------------------------------------------------------------
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // Resolve the symbology name to a BaseEncodeType value using reflection.
        // This allows the user to specify the symbology as a string matching the EncodeTypes fields.
        // --------------------------------------------------------------------
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // --------------------------------------------------------------------
        // Generate the barcode and save it to the specified path.
        // --------------------------------------------------------------------
        try
        {
            using (var generator = new BarcodeGenerator(encodeType, data))
            {
                // Prevent an exception if the provided data does not strictly conform to the symbology rules.
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                // Save the barcode as a PNG image (default format).
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