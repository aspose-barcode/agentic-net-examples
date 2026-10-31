// Title: Generate Barcode Image from Command-Line Arguments
// Description: Demonstrates creating a barcode image using Aspose.BarCode based on supplied symbology and code text, then saving it to a file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcode images. Typical use cases include generating barcodes for inventory, shipping labels, or QR codes in desktop or server applications. Developers often need to configure dimensions, resolution, and output formats, making this pattern a common starting point for barcode creation tasks.
// Prompt: Develop a RESTful endpoint that accepts multipart/form-data and returns generated barcode image stream.
// Tags: barcode, generation, png, command-line, aspose.barcode, encode-types

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a barcode image using Aspose.BarCode based on command‑line parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Expects at least two arguments: the symbology name and the code text.
    /// An optional third argument specifies the output file path (defaults to "barcode.png").
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Validate that the required arguments are present.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <SymbologyName> <CodeText> [OutputPath]");
            return;
        }

        // Parse arguments.
        string symbologyName = args[0];
        string codeText = args[1];
        string outputPath = args.Length >= 3 ? args[2] : "barcode.png";

        // Resolve the requested symbology to a concrete EncodeType.
        BaseEncodeType encodeType;
        try
        {
            encodeType = ResolveEncodeType(symbologyName);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        try
        {
            // Create a BarcodeGenerator with the resolved type and provided text.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Configure barcode appearance and image resolution.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Resolution = 300f;

                // Save the generated barcode to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading.

                    // Write the memory stream contents to the target file.
                    using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        ms.CopyTo(fileStream);
                    }
                }
            }

            Console.WriteLine($"Barcode generated and saved to '{Path.GetFullPath(outputPath)}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }

    /// <summary>
    /// Resolves a symbology name to the corresponding <see cref="BaseEncodeType"/> using reflection.
    /// </summary>
    /// <param name="symbologyName">The name of the symbology (e.g., "Code128").</param>
    /// <returns>The matching <see cref="BaseEncodeType"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when the symbology name is not found.</exception>
    static BaseEncodeType ResolveEncodeType(string symbologyName)
    {
        // Look for a public static field on EncodeTypes that matches the provided name.
        var field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {symbologyName}");
        return (BaseEncodeType)field.GetValue(null);
    }
}