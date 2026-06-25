using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a high‑resolution Code128 barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image with 600 DPI resolution.
    /// </summary>
    static void Main()
    {
        // Build the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_600dpi.png");

        // Initialise a BarcodeGenerator for Code128 symbology with the sample data "ABC123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123456"))
        {
            // Configure the generator to produce a high‑resolution image suitable for glossy label printing.
            generator.Parameters.Resolution = 600f;          // 600 dots per inch

            // Enable anti‑aliasing to smooth the barcode edges.
            generator.Parameters.UseAntiAlias = true;

            // Persist the generated barcode to a PNG file at the specified location.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}