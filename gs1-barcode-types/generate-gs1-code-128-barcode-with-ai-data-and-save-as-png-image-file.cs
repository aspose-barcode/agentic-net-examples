using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a GS1 Code 128 barcode with sample AI data
    /// and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated barcode image.
        string outputPath = "gs1code128.png";

        // Sample GS1 Application Identifier (AI) data:
        // (01) – GTIN, (10) – Batch/Lot number.
        string codeText = "(01)12345678901231(10)BATCH123";

        // Initialize the barcode generator for GS1 Code 128 with the provided AI data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Ensure the checksum digit is displayed in the human‑readable text.
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"GS1 Code 128 barcode saved to: {outputPath}");
    }
}