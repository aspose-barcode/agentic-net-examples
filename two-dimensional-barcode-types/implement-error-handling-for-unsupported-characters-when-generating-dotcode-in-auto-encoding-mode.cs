// Title: Generate DotCode barcode with Auto encoding and unsupported character handling
// Description: Demonstrates creating a DotCode barcode in Auto encoding mode, specifying ISO‑8859‑1 ECI, and catching exceptions when the input contains characters that cannot be encoded.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on DotCode symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and DotCode parameters such as EncodeMode and ECIEncoding. Developers often need to generate DotCode barcodes for compact data storage and must handle encoding limitations, making this pattern useful for error‑resilient barcode creation.
// Prompt: Implement error handling for unsupported characters when generating DotCode in Auto encoding mode.
// Tags: dotcode, barcode, generation, error handling, auto encoding, eci, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DotCode barcode with Auto encoding and handling unsupported characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, saves it as PNG, and reports success or encoding errors.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "DotCodeExample");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "DotCodeAuto.png");

        // Text containing characters not supported by ISO‑8859‑1 (e.g., Japanese kanji)
        string codeText = "犬Right狗";

        // Initialize the barcode generator for DotCode with the provided text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Set Auto encoding mode and limit ECI to ISO‑8859‑1
            generator.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.Auto;
            generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.ISO_8859_1;

            // Attempt to generate and save the barcode, handling unsupported characters
            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated successfully: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unsupported character encountered: {ex.Message}");
            }
        }
    }
}