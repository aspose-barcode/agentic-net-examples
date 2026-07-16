// Title: Generate barcode image from command‑line arguments
// Description: Demonstrates how to create a barcode image using Aspose.BarCode by specifying symbology, data, and output path via command‑line.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BaseEncodeType to produce barcode images. Typical use cases include automating barcode creation in batch scripts, CI pipelines, or backend services where input parameters are supplied at runtime. Developers often need to map symbology names to EncodeTypes and ensure output directories exist.
// Prompt: Write a sample console program that accepts command‑line arguments for barcode type, data, and output path.
// Tags: barcode generation command-line symbology encode-types output-file aspose.barcode

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
    /// Entry point of the program.
    /// Accepts three optional arguments: symbology name, data to encode, and output file path.
    /// Returns 0 on success, 1 on error.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    /// <returns>Exit code indicating success or failure.</returns>
    static int Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Resolve input arguments or fall back to default values
        // --------------------------------------------------------------------
        string symbologyName = args.Length > 0 ? args[0] : "Code128";
        string codeText = args.Length > 1 ? args[1] : "Sample123";
        string outputPath = args.Length > 2 ? args[2] : "barcode.png";

        // --------------------------------------------------------------------
        // Convert the symbology name to the corresponding EncodeTypes value using reflection
        // --------------------------------------------------------------------
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return 1;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
        if (encodeType == null)
        {
            Console.WriteLine($"Failed to obtain encode type for: {symbologyName}");
            return 1;
        }

        // --------------------------------------------------------------------
        // Ensure the directory for the output file exists
        // --------------------------------------------------------------------
        string? directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // --------------------------------------------------------------------
        // Generate the barcode and save it to the specified path
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Save(outputPath);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
        return 0;
    }
}