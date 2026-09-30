// Title: Toggle checksum for a barcode symbology and generate PNG image
// Description: Demonstrates how to use Aspose.BarCode to generate a barcode image with checksum enabled or disabled based on command-line arguments.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and checksum settings. Typical use cases include creating barcodes for inventory, shipping, or retail where checksum validation may be required. Developers often need to programmatically control checksum options and output image files.
// Prompt: Build a console utility that accepts arguments to toggle checksum for a symbology and outputs the image path.
// Tags: barcode, symbology, checksum, console, aspose.barcode, generation, png

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Console utility that generates a barcode image with optional checksum based on command-line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses arguments, resolves the symbology, configures checksum, generates and saves the barcode image.
    /// </summary>
    /// <param name="args">Command-line arguments: [0] symbology name (default Code39FullASCII), [1] checksum flag (on/off).</param>
    static void Main(string[] args)
    {
        // Default values for symbology and checksum flag
        string symbologyName = "Code39FullASCII";
        string checksumArg = "off";

        // Override defaults with provided arguments, if any
        if (args.Length >= 1 && !string.IsNullOrWhiteSpace(args[0]))
        {
            symbologyName = args[0];
        }

        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
        {
            checksumArg = args[1];
        }

        // Resolve the symbology name to a BaseEncodeType instance using reflection
        BaseEncodeType encodeType = ResolveEncodeType(symbologyName);
        if (encodeType == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        // Determine whether checksum should be enabled based on the second argument
        bool enableChecksum = checksumArg.Equals("on", StringComparison.OrdinalIgnoreCase) ||
                              checksumArg.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                              checksumArg.Equals("true", StringComparison.OrdinalIgnoreCase);

        // Sample code text (must be valid for the chosen symbology)
        string codeText = "12345";

        // Prepare the output directory and file name
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string fileName = $"{encodeType.TypeName}_Checksum_{(enableChecksum ? "On" : "Off")}.png";
        string outputPath = Path.Combine(outputDir, fileName);

        // Generate the barcode with the selected symbology and checksum setting
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Apply checksum configuration
            generator.Parameters.Barcode.IsChecksumEnabled = enableChecksum
                ? EnableChecksum.Yes
                : EnableChecksum.No;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Resolves a symbology name to the corresponding BaseEncodeType using reflection.
    /// Supports both fields and properties on the EncodeTypes class.
    /// </summary>
    /// <param name="name">The name of the symbology (case-insensitive).</param>
    /// <returns>The matching BaseEncodeType, or null if not found.</returns>
    private static BaseEncodeType ResolveEncodeType(string name)
    {
        // Attempt to locate a public static field with the given name
        FieldInfo field = typeof(EncodeTypes).GetField(name, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
        if (field != null && typeof(BaseEncodeType).IsAssignableFrom(field.FieldType))
        {
            return (BaseEncodeType)field.GetValue(null);
        }

        // If not found as a field, try a public static property as a fallback
        PropertyInfo prop = typeof(EncodeTypes).GetProperty(name, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
        if (prop != null && typeof(BaseEncodeType).IsAssignableFrom(prop.PropertyType))
        {
            return (BaseEncodeType)prop.GetValue(null);
        }

        // No matching field or property was found
        return null;
    }
}