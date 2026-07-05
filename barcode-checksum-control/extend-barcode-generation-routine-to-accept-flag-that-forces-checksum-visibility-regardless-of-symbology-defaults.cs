// Title: Force checksum visibility in barcode generation
// Description: Demonstrates how to generate a Code128 barcode and optionally force the checksum digit to appear in the human‑readable text.
// Prompt: Extend the barcode generation routine to accept a flag that forces checksum visibility regardless of symbology defaults.
// Tags: barcode symbology, checksum, output format, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program entry point demonstrating barcode generation with optional checksum visibility.
/// </summary>
class Program
{
    /// <summary>
    /// Parses command‑line arguments to determine if the checksum should always be shown,
    /// generates a Code128 barcode, and saves it as an image file.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine whether to force checksum visibility based on a command‑line flag.
        bool forceChecksum = false;
        foreach (string arg in args)
        {
            if (arg.Equals("--show-checksum", StringComparison.OrdinalIgnoreCase))
            {
                forceChecksum = true;
                break;
            }
        }

        // Sample barcode data to encode.
        string codeText = "123456789";

        // Create a Code128 barcode generator with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // If the flag is set, force the checksum digit to appear in the human‑readable text.
            if (forceChecksum)
            {
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            }

            // Define the output file path and save the barcode image.
            string outputPath = "code128.png";
            generator.Save(outputPath);

            // Inform the user about the saved file and checksum visibility status.
            Console.WriteLine($"Barcode saved to {outputPath}. Checksum visibility forced: {forceChecksum}");
        }
    }
}