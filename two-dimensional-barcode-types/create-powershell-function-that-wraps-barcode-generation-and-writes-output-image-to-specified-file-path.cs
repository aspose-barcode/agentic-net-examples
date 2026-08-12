// Title: Generate Barcode Image with Aspose.BarCode in C#
// Description: Demonstrates how to generate a barcode image using Aspose.BarCode and save it to a file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and related parameter classes to create various barcode symbologies. Typical scenarios include creating product labels, QR codes, and inventory tags where developers need to programmatically produce barcode images in common formats such as PNG or JPEG.
// Prompt: Create a PowerShell function that wraps barcode generation and writes output image to specified file path.
// Tags: barcode symbology, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple console application that generates a barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Demonstrates barcode generation with sample parameters.
    /// </summary>
    static void Main()
    {
        // Define sample input values.
        string symbology = "Code128";
        string codeText = "123ABC";

        // Build an output path in the temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate the barcode and save it to the specified file.
        GenerateBarcode(symbology, codeText, outputPath);

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Generates a barcode image using Aspose.BarCode and saves it to the specified file.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "Code128", "QR").</param>
    /// <param name="codeText">Text to encode in the barcode.</param>
    /// <param name="outputFilePath">Full path where the image will be saved.</param>
    static void GenerateBarcode(string symbologyName, string codeText, string outputFilePath)
    {
        // Validate input arguments.
        if (string.IsNullOrWhiteSpace(symbologyName))
            throw new ArgumentException("Symbology name must be provided.", nameof(symbologyName));

        if (string.IsNullOrWhiteSpace(codeText))
            throw new ArgumentException("Code text must be provided.", nameof(codeText));

        if (string.IsNullOrWhiteSpace(outputFilePath))
            throw new ArgumentException("Output file path must be provided.", nameof(outputFilePath));

        // Resolve the symbology name to an EncodeTypes field via reflection.
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        // Cast the resolved field value to BaseEncodeType.
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Ensure the target directory exists.
        string directory = Path.GetDirectoryName(outputFilePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Generate and save the barcode image.
        try
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Optional: configure additional parameters here, e.g.:
                // generator.Parameters.Barcode.XDimension.Point = 2f;

                generator.Save(outputFilePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}