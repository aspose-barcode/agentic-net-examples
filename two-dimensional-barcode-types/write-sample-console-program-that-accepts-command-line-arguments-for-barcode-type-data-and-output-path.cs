// Title: Generate barcode image from command‑line arguments
// Description: Demonstrates how to create a barcode image using Aspose.BarCode by passing the symbology, data, and output file path via command‑line arguments.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. It shows typical scenarios such as dynamic barcode creation in console utilities, batch processing, and integration into CI pipelines where image format is inferred from file extension.
// Prompt: Write a sample console program that accepts command‑line arguments for barcode type, data, and output path.
// Tags: barcode generation, command line, encode types, image format, aspose.barcode, console app

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample console application that generates a barcode image based on command‑line parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses arguments, resolves the symbology, determines image format, and saves the barcode.
    /// </summary>
    /// <param name="args">Command‑line arguments: [0] symbology name, [1] data string, [2] output file path.</param>
    static void Main(string[] args)
    {
        // Default values used when arguments are missing
        string symbologyName = "Code128";
        string codeText = "12345678";
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Override defaults with supplied arguments, if any
        if (args.Length >= 1 && !string.IsNullOrWhiteSpace(args[0]))
            symbologyName = args[0];
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
            codeText = args[1];
        if (args.Length >= 3 && !string.IsNullOrWhiteSpace(args[2]))
            outputPath = args[2];

        // Resolve symbology name to BaseEncodeType via reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
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

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Determine image format from file extension (default to PNG)
        BarCodeImageFormat imageFormat = BarCodeImageFormat.Png;
        string ext = Path.GetExtension(outputPath).ToLowerInvariant();
        switch (ext)
        {
            case ".png":
                imageFormat = BarCodeImageFormat.Png;
                break;
            case ".jpg":
            case ".jpeg":
                imageFormat = BarCodeImageFormat.Jpeg;
                break;
            case ".bmp":
                imageFormat = BarCodeImageFormat.Bmp;
                break;
            case ".gif":
                imageFormat = BarCodeImageFormat.Gif;
                break;
            case ".tiff":
            case ".tif":
                imageFormat = BarCodeImageFormat.Tiff;
                break;
            default:
                Console.WriteLine($"Unrecognized or unsupported file extension '{ext}'. Defaulting to PNG.");
                break;
        }

        // Generate the barcode and save it to the specified path
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Save(outputPath, imageFormat);
        }

        Console.WriteLine($"Barcode generated: Type={symbologyName}, Data=\"{codeText}\", SavedTo=\"{outputPath}\"");
    }
}