using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a barcode image using Aspose.BarCode with optional command‑line parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional arguments to customize the barcode generation:
    /// <list type="number">
    ///   <item>symbologyName – the barcode symbology (e.g., Code39FullASCII)</item>
    ///   <item>codeText – the text to encode</item>
    ///   <item>checksumFlag – \"yes\" or \"no\" to enable checksum</item>
    ///   <item>outputPath – file path for the generated image</item>
    /// </list>
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Default values for barcode generation
        // --------------------------------------------------------------------
        string symbologyName = "Code39FullASCII";
        string codeText = "12345";
        string checksumArg = "yes";
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // --------------------------------------------------------------------
        // Override defaults with command‑line arguments, if supplied
        // Expected order: symbologyName codeText checksumFlag outputPath
        // --------------------------------------------------------------------
        if (args.Length > 0) symbologyName = args[0];
        if (args.Length > 1) codeText = args[1];
        if (args.Length > 2) checksumArg = args[2];
        if (args.Length > 3) outputPath = args[3];

        // --------------------------------------------------------------------
        // Resolve the symbology name to the corresponding EncodeTypes value
        // using reflection on the EncodeTypes class
        // --------------------------------------------------------------------
        FieldInfo field = typeof(EncodeTypes).GetField(
            symbologyName,
            BindingFlags.Public | BindingFlags.Static);

        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // --------------------------------------------------------------------
        // Determine whether checksum should be enabled based on the argument
        // --------------------------------------------------------------------
        EnableChecksum checksumSetting = string.Equals(
            checksumArg,
            "no",
            StringComparison.OrdinalIgnoreCase)
            ? EnableChecksum.No
            : EnableChecksum.Yes;

        // --------------------------------------------------------------------
        // Ensure the directory for the output file exists
        // --------------------------------------------------------------------
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // Generate the barcode and save it to the specified path
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;
            generator.Save(outputPath);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}