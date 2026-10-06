// Title: Generate barcode image from command‑line arguments
// Description: Demonstrates how to create a barcode image using Aspose.BarCode by specifying symbology, data, and output file via command‑line parameters.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include creating barcodes for inventory, shipping labels, or product packaging. Developers often need to accept dynamic input, resolve symbology types, and save images in various formats, which this sample covers.
// Prompt: Write a sample console program that accepts command‑line arguments for barcode type, data, and output path.
// Tags: barcode generation, command-line, encode types, image format, aspose.barcode, console app

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample console application that generates a barcode image based on command‑line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses arguments, resolves the requested symbology, and saves the generated barcode.
    /// </summary>
    /// <param name="args">
    /// args[0] – symbology name (e.g., "Code128"); args[1] – data to encode; args[2] – output file path.
    /// </param>
    static void Main(string[] args)
    {
        // Default values used when arguments are missing
        string symbologyName = "Code128";
        string codeText = "12345678";
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Override defaults with supplied arguments, if any
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            symbologyName = args[0];
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            codeText = args[1];
        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            outputPath = args[2];

        // Resolve the symbology name to the corresponding EncodeTypes enum value using reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
        BarCodeImageFormat imageFormat = GetImageFormatFromPath(outputPath);

        // Ensure the output directory exists before attempting to save the file
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
        {
            Directory.CreateDirectory(outDir);
        }

        // Generate the barcode and save it using the selected image format
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Example of setting a simple property (optional)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            generator.Save(outputPath, imageFormat);
        }

        Console.WriteLine($"Barcode generated: {outputPath}");
    }

    /// <summary>
    /// Determines the appropriate <see cref="BarCodeImageFormat"/> based on the file extension of the output path.
    /// </summary>
    /// <param name="path">The full output file path.</param>
    /// <returns>The matching image format; defaults to PNG for unknown extensions.</returns>
    static BarCodeImageFormat GetImageFormatFromPath(string path)
    {
        string ext = Path.GetExtension(path).ToLowerInvariant();
        switch (ext)
        {
            case ".png":
                return BarCodeImageFormat.Png;
            case ".jpg":
            case ".jpeg":
                return BarCodeImageFormat.Jpeg;
            case ".bmp":
                return BarCodeImageFormat.Bmp;
            case ".gif":
                return BarCodeImageFormat.Gif;
            case ".tiff":
            case ".tif":
                return BarCodeImageFormat.Tiff;
            case ".svg":
                return BarCodeImageFormat.Svg;
            case ".pdf":
                return BarCodeImageFormat.Pdf;
            case ".emf":
                return BarCodeImageFormat.Emf;
            default:
                // Default to PNG if the extension is not recognized
                return BarCodeImageFormat.Png;
        }
    }
}