// Title: Barcode Generator with Checksum Toggle
// Description: Demonstrates how to generate a barcode image while allowing the checksum to be turned on or off via command‑line arguments.
// Prompt: Build a console utility that accepts arguments to toggle checksum for a symbology and outputs the image path.
// Tags: barcode symbology, checksum, console, aspose.barcode, image output

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Console utility that creates a barcode image based on command‑line arguments.
/// Allows toggling of the checksum feature for the selected symbology and prints the saved image path.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts up to four arguments: symbology name, checksum flag, code text, and optional output path.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Default values for optional parameters
        // --------------------------------------------------------------------
        string symbologyName = "Code128";   // Default symbology
        string checksumArg = "on";          // Default checksum setting
        string codeText = "123456";         // Default data to encode
        string outputPath = null;           // Will be generated if not supplied

        // --------------------------------------------------------------------
        // Parse command‑line arguments (if any) and override defaults
        // --------------------------------------------------------------------
        if (args.Length > 0) symbologyName = args[0];
        if (args.Length > 1) checksumArg = args[1];
        if (args.Length > 2) codeText = args[2];
        if (args.Length > 3) outputPath = args[3];

        // --------------------------------------------------------------------
        // Resolve the symbology name to an EncodeTypes value using reflection
        // --------------------------------------------------------------------
        var field = typeof(EncodeTypes).GetField(
            symbologyName,
            BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);

        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // --------------------------------------------------------------------
        // Determine whether checksum should be enabled based on the argument
        // --------------------------------------------------------------------
        EnableChecksum checksumSetting = EnableChecksum.Yes;

        if (string.Equals(checksumArg, "off", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(checksumArg, "no", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(checksumArg, "false", StringComparison.OrdinalIgnoreCase))
        {
            checksumSetting = EnableChecksum.No;
        }

        // --------------------------------------------------------------------
        // Build the output file path if the user did not provide one
        // --------------------------------------------------------------------
        if (string.IsNullOrWhiteSpace(outputPath))
        {
            string fileName = $"{encodeType.TypeName}_{(checksumSetting == EnableChecksum.Yes ? "ChecksumOn" : "ChecksumOff")}.png";
            outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        }

        // --------------------------------------------------------------------
        // Generate the barcode image with the selected settings
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;
            generator.Save(outputPath);
        }

        // --------------------------------------------------------------------
        // Inform the user where the image was saved
        // --------------------------------------------------------------------
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}