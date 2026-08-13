// Title: Barcode generation with optional forced checksum visibility
// Description: Demonstrates how to generate a Code128 barcode and optionally force the checksum digit to appear in the human‑readable text.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating use of BarcodeGenerator, EncodeTypes, and checksum display settings. Developers often need to customize barcode appearance, such as showing or hiding checksum digits, for compliance or readability in labeling applications.
// Prompt: Extend the barcode generation routine to accept a flag that forces checksum visibility regardless of symbology defaults.
// Tags: barcode symbology, generation, checksum, code128, image output, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a barcode image and optionally forces the checksum digit to be displayed
/// in the human‑readable text, regardless of the symbology's default behavior.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Accepts an optional command‑line argument
    /// "showchecksum" to enable forced checksum visibility.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine whether to force checksum visibility based on the first argument.
        bool forceShowChecksum = false;
        if (args.Length > 0 && string.Equals(args[0], "showchecksum", StringComparison.OrdinalIgnoreCase))
        {
            forceShowChecksum = true;
        }

        // Define barcode parameters: Code128 symbology and sample text.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "123ABC";

        // Create the barcode generator within a using block to ensure proper disposal.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // If the flag is set, force the checksum digit to be shown in the human‑readable text.
            if (forceShowChecksum)
            {
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            }

            // Optional: adjust image dimensions for better visibility.
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Build the output file path in the current working directory.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);

            // Inform the user about the saved file and the checksum flag status.
            Console.WriteLine($"Barcode saved to: {outputPath}");
            Console.WriteLine($"Force checksum visibility: {forceShowChecksum}");
        }
    }
}