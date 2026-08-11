// Title: Command‑Line Barcode Generation Utility
// Description: Generates a barcode image using Aspose.BarCode based on command‑line parameters for symbology, data, and output file path.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to create barcodes programmatically. It showcases the BarcodeGenerator, EncodeTypes, and BaseEncodeType classes, which are commonly used for producing barcode images for labeling, inventory, and point‑of‑sale applications. Developers often need to select a symbology at runtime, supply data, and save the result in various image formats.
/// Prompt: Create a custom barcode generation utility that accepts command‑line arguments for symbology, data, and output path.
/// Tags: barcode, symbology, generation, command-line, aspose.barcode, encode-types, image-output

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple command‑line utility for generating barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Parses command‑line arguments, creates a barcode generator,
    /// and saves the resulting image to the specified path.
    /// </summary>
    /// <param name="args">
    /// Expected arguments:
    /// 0 – Symbology name (e.g., "Code128").
    /// 1 – Data to encode.
    /// 2 – Output file path (image format inferred from extension).
    /// </param>
    /// <returns>0 on success; non‑zero error code on failure.</returns>
    static int Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Resolve command‑line arguments or fall back to default values.
        // --------------------------------------------------------------------
        string symbology = args.Length > 0 ? args[0] : "Code128";
        string data = args.Length > 1 ? args[1] : "Sample123";
        string outputPath = args.Length > 2 ? args[2] : "barcode.png";

        // --------------------------------------------------------------------
        // Convert the symbology string to the corresponding EncodeTypes field.
        // --------------------------------------------------------------------
        FieldInfo field = typeof(EncodeTypes).GetField(symbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbology}");
            return 1;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
        if (encodeType == null)
        {
            Console.WriteLine($"Failed to obtain encode type for symbology: {symbology}");
            return 1;
        }

        // --------------------------------------------------------------------
        // Ensure the output directory exists before attempting to save the file.
        // --------------------------------------------------------------------
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        try
        {
            // ----------------------------------------------------------------
            // Create and configure the barcode generator.
            // ----------------------------------------------------------------
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, data))
            {
                // Optional: set a common parameter (module size) for better readability.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the barcode image; format is inferred from the file extension.
                generator.Save(outputPath);
            }

            Console.WriteLine($"Barcode generated successfully: {outputPath}");
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode: {ex.Message}");
            return 1;
        }
    }
}