using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a DataBar Expanded Stacked barcode and saves it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DataBar Expanded Stacked barcode with specific parameters and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file name and location.
        string outputPath = "databar_expanded_stacked.bmp";

        // Initialize a barcode generator for the DataBar Expanded Stacked symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked))
        {
            // Assign the code text. This example uses a GTIN (AI 01) value.
            generator.CodeText = "(01)12345678901231";

            // Configure DataBar‑specific settings:
            //   - Columns: number of stacked columns (3 in this case).
            //   - AspectRatio: height‑to‑width ratio (set to 8).
            generator.Parameters.Barcode.DataBar.Columns = 3;
            generator.Parameters.Barcode.DataBar.AspectRatio = 8f;

            // Render the barcode and save it as a BMP image.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user that the barcode image has been created.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}